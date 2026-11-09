import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { Booking, RentalCompany } from '../../../Enviroment/model';
import { AdminService } from '../../../core/services/admin-service';
import {
  GenericTable,
  tableAction,
  tableColumn,
} from '../../../shared/generic-table/generic-table';
import { UtitlityService } from '../../../core/services/utitlity-service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admin-booking',
  imports: [GenericTable],
  templateUrl: './admin-booking.html',
  styleUrl: './admin-booking.css',
})
export class AdminBooking implements OnInit {
  pageNumber = signal(1);
  pageSize = signal(5);
  totalCount = signal(0);
  totalPages = computed(() => Math.ceil(this.totalCount() / this.pageSize()));
  hasPrevious = computed(() => this.pageNumber() > 1);
  hasNext = computed(() => this.pageNumber() < this.totalPages());

  searchBy = signal('');
  sortBy = signal<string>('');
  sortDirection = signal<'asc' | 'desc'>('asc');
  bookingStatus = signal('');
  RentalCompanyId = signal<string>('');
  RentalCompany = signal<RentalCompany[]>([]);
  loading = signal(false);
  bookings = signal<Booking[]>([]);

  isAsc = signal(true);

  adminService = inject(AdminService);
  utilityService = inject(UtitlityService);
  toastr = inject(ToastrService);

  currentBooking = signal<Booking | null>(null);
  showBookingStatusModal = signal(false);
  selectedBookingStatus = signal('');
  fromDate = signal('');
  toDate = signal('');
  totalBooking = signal(0);
  totalRevenue = signal(0);
  fleetUtilizationRate = signal(0);

  column: tableColumn<Booking>[] = [
    {
      key: 'id',
      label: 'Booking Id',
      sortable: true,
      sortKey: 'id',
    },
    {
      key: 'pickupLocation',
      label: 'Pickup Location',
      sortable: true,
      sortKey: 'pickupLocation',
    },
    {
      key: 'returnLocation',
      label: 'Return Location',
      sortable: true,
      sortKey: 'returnLocation',
    },
    {
      key: 'bookingStatus',
      label: 'Booking Status',
      sortable: true,
      sortKey: 'bookingStatus',
      format: (value) => this.utilityService.getBookingStatus(value as number),
    },
    {
      key: 'totalDays',
      label: 'Total Days',
      sortable: true,
      sortKey: 'totalDays',
    },
    {
      key: 'totalAmount',
      label: 'Total Amount',
      sortable: true,
      sortKey: 'totalAmount',
    },
    {
      key: 'pickupDateTime',
      label: 'pickup Date',
      sortable: true,
      sortKey: 'pickupDateTime',
      format: (value) => this.utilityService.formatLocalDate(value as string),
    },
    {
      key: 'returnDateTime',
      label: 'Return Date',
      sortable: true,
      sortKey: 'returnDateTime',
      format: (value) => this.utilityService.formatLocalDate(value as string),
    },
    {
      key: 'bookedOn',
      label: 'Booked On',
      sortable: true,
      sortKey: 'bookedOn',
      format: (value) => this.utilityService.formatLocalDate(value as string),
    },
    {
      key: 'companyName',
      label: 'Company Name',
      sortable: true,
      sortKey: 'companyName',
    },
    {
      key: 'vehicleModel',
      label: 'Vehicle Model',
      sortable: true,
      sortKey: 'vehicleModel',
    },
    {
      key: 'id',
      label: 'action',
      type: 'actions',
    },
  ];

  action: tableAction<Booking>[] = [
    {
      label: 'Confirm',
      action: 'Confirm',
      class:
        'bg-blue-500 text-white font-bold px-6 py-2 rounded-xl cursor-pointer hover: bg-blue-700 transition',
      showIf: (row: Booking) => row.bookingStatus == 0,
    },
    {
      label: 'Active',
      action: 'Active',
      class:
        'bg-blue-500 text-white font-bold px-6 py-2 rounded-xl cursor-pointer hover: bg-blue-700 transition',
      showIf: (row: Booking) => row.bookingStatus == 1,
    },
    {
      label: 'Cancel',
      action: 'Cancel',
      class:
        'bg-red-500 text-white font-bold px-6 py-2 rounded-xl cursor-pointer hover: bg-red-700 transition',
      showIf: (row: Booking) => row.bookingStatus == 0 || row.bookingStatus == 1,
    },
    {
      label: 'Complete',
      action: 'Complete',
      class:
        'bg-blue-500 text-white font-bold px-6 py-2 rounded-xl cursor-pointer hover: bg-blue-700 transition',
      showIf: (row: Booking) => row.bookingStatus == 2,
    },
  ];

  onTableAction(event: { action: string; row: Booking }) {
    this.showBookingStatusModal.set(true);
    this.currentBooking.set(event.row);

    if (event.action == 'Confirm') {
      this.selectedBookingStatus.set('Confirm');
    } else if (event.action == 'Cancel') {
      this.selectedBookingStatus.set('Cancel');
    } else if (event.action == 'Active') {
      this.selectedBookingStatus.set('Active');
    } else if (event.action == 'Complete') {
      this.selectedBookingStatus.set('Complete');
    }
  }

  selectedBookingStatusFun() {
    const action = this.selectedBookingStatus();

    if (action == 'Confirm') {
      this.updateBookingStatus(1);
    } else if (action == 'Cancel') {
      this.updateBookingStatus(4);
    } else if (action == 'Active') {
      this.updateBookingStatus(2);
    } else if (action == 'Complete') {
      this.updateBookingStatus(3);
    }
  }

  updateBookingStatus(value: number) {
    this.adminService
      .updateBookingStatus({
        BookingId: this.currentBooking()?.id,
        BookingStatus: value,
      })
      .subscribe({
        next: (res) => {
          this.toastr.success('Status Updated');
          this.showBookingStatusModal.set(false);
          this.loadBooking();
        },
        error: (err) => {
          this.toastr.error(err.error?.detail || 'something went wrong');
          this.showBookingStatusModal.set(false);
          this.loadBooking();
        },
      });
  }

  ngOnInit(): void {
    this.loadBooking();
    this.loadRentalCompany();
    this.loadBookingDashBoard();
  }

  loadBooking() {
    this.adminService
      .getPagedBookings({
        PageNumber: this.pageNumber(),
        PageSize: this.pageSize(),
        SortBy: this.sortBy(),
        SortDirection: this.sortDirection(),
        SearchBy: this.searchBy(),
        BookingStatus: this.bookingStatus(),
        RentalCompanyId: this.RentalCompanyId(),
        ToDate: this.toDate(),
        FromDate: this.fromDate(),
      })
      .subscribe({
        next: (res) => {
          this.bookings.set(res.items);
          this.pageNumber.set(res.pageNumber);
          this.totalCount.set(res.totalCount);
        },
        error: (err) => {
          console.log(err);
        },
      });
  }

  loadBookingDashBoard() {
    this.adminService
      .getBookingDashBoard({
        RentalCompanyId: this.RentalCompanyId(),
      })
      .subscribe({
        next: (res) => {
          console.log(res);
          this.totalBooking.set(res.totalBookings);
          console.log(this.totalBooking());
          this.totalRevenue.set(res.totalRevenue);
          this.fleetUtilizationRate.set(res.fleetUtilizationRate);
        },
      });
  }

  loadRentalCompany() {
    this.adminService.getRentalCompany().subscribe({
      next: (res) => {
        this.RentalCompany.set(res);
      },
    });
  }

  onFromDateChange(value: string) {
    this.fromDate.set(value);
  }

  onToDateChange(value: string) {
    this.toDate.set(value);
  }

  applyDateFilter() {
    if (this.fromDate() && this.toDate()) {
      if (this.fromDate() > this.toDate()) {
        alert(`to Date should be greater than from Date`);
        this.fromDate.set('');
        this.toDate.set('');
        return;
      }
    }
    this.loadBooking();
  }

  onSortBy(value: string) {
    if (this.sortBy() == value) this.isAsc.update((x) => !x);
    else {
      this.isAsc.set(true);
      this.sortBy.set(value);
    }
    this.sortDirection.set(this.isAsc() == true ? 'asc' : 'desc');
    this.pageNumber.set(1);
    this.loadBooking();
  }

  formatDateForInput(date: Date | string): string {
    if (typeof date === 'string') {
      return date.split('T')[0];
    }

    return `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, '0')}-${String(date.getDate()).padStart(2, '0')}`;
  }

  onSearchBy(value: string) {
    this.searchBy.set(value);
    this.pageNumber.set(1);
    this.loadBooking();
  }

  onBookingStatusChange(value: string) {
    this.bookingStatus.set(value);
    this.pageNumber.set(1);
    this.loadBooking();
  }

  onRentalCompanyId(value: string) {
    this.RentalCompanyId.set(value);
    this.pageNumber.set(1);
    this.loadBooking();
    this.loadBookingDashBoard();
  }

  previousPage() {
    if (this.hasPrevious()) {
      this.pageNumber.update((x) => x - 1);
      this.loadBooking();
    }
  }
  nextPage() {
    if (this.hasNext()) {
      this.pageNumber.update((x) => x + 1);
      this.loadBooking();
    }
  }
}
