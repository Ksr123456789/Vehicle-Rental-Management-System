import { Component, computed, inject, signal } from '@angular/core';
import { CustomerService } from '../../../core/services/customer-service';
import { tableColumn } from '../../../shared/generic-table/generic-table';
import { MyRentals } from '../../../Enviroment/model';
import { DeleteModal } from '../../../shared/delete-modal/delete-modal';
import { UtitlityService } from '../../../core/services/utitlity-service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-customer-rentals',
  imports: [DeleteModal],
  templateUrl: './customer-rentals.html',
  styleUrl: './customer-rentals.css',
})
export class CustomerRentals {
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
  loading = signal(false);
  myRentals = signal<MyRentals[]>([]);
  currentRental = signal<MyRentals | null>(null);
  SeatingCapacity = signal('');

  isAsc = signal(true);

  customerService = inject(CustomerService);
  utitlityService = inject(UtitlityService);
  toastr = inject(ToastrService);

  showRentalDetailsModal = signal(false);
  showDeleteModal = signal(false);

  seatings = signal(Array.from({ length: 20 }, (_, index) => 20 - index));

  ngOnInit(): void {
    this.loadBooking();
  }

  loadBooking() {
    this.customerService
      .getMyPagedRentals({
        PageNumber: this.pageNumber(),
        PageSize: this.pageSize(),
        SortBy: this.sortBy(),
        SortDirection: this.sortDirection(),
        SearchBy: this.searchBy(),
        BookingStatus: this.bookingStatus(),
        SeatingCapacity: this.SeatingCapacity()
      })
      .subscribe({
        next: (res) => {
          this.myRentals.set(res.items);
          this.pageNumber.set(res.pageNumber);
          this.totalCount.set(res.totalCount);

          console.log(res);
        },
        error: (err) => {
          console.log(err);
        },
      });
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

  onSeatingCapacityChange(value: string) {
    this.SeatingCapacity.set(value);
    this.pageNumber.set(1);
    this.loadBooking();
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

  disableButton(value: MyRentals) {
    return value.bookingStatus != 0;
  }

  cancelRental() {
    console.log(this.currentRental());
    this.customerService
      .cancelRentals({
        BookingId: this.currentRental()?.id,
      })
      .subscribe({
        next: (res) => {
          console.log(res);
          this.closeDeleteModal();
          this.toastr.success('Rental cancelled');
        },
        error: (err) => {
          console.log(err);
          this.closeDeleteModal();
          this.toastr.error(err.error?.detail || 'something went wrong');
        },
      });
  }

  openRentalDetailsModal(value: MyRentals) {
    this.currentRental.set(value);
    this.showRentalDetailsModal.set(true);
  }

  closeRentalDetailsModal() {
    this.showRentalDetailsModal.set(false);
  }

  openDeleteModal(rental: MyRentals) {
    this.currentRental.set(rental);
    console.log(this.currentRental());
    this.showDeleteModal.set(true);
  }

  closeDeleteModal() {
    this.loadBooking();
    this.showDeleteModal.set(false);
  }
}
