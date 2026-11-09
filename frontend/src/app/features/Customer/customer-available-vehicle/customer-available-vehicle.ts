import { Component, computed, inject, signal } from '@angular/core';
import { AvailableVehicle, RentalCompany, VehicleCategory } from '../../../Enviroment/model';
import {
  GenericTable,
  tableAction,
  tableColumn,
} from '../../../shared/generic-table/generic-table';
import { CustomerService } from '../../../core/services/customer-service';
import { BookVehicle } from '../book-vehicle/book-vehicle';
import { UtitlityService } from '../../../core/services/utitlity-service';

@Component({
  selector: 'app-customer-available-vehicle',
  imports: [GenericTable, BookVehicle],
  templateUrl: './customer-available-vehicle.html',
  styleUrl: './customer-available-vehicle.css',
})
export class CustomerAvailableVehicle {
  pageNumber = signal(1);
  pageSize = signal(5);
  totalCount = signal(0);
  totalPages = computed(() => Math.ceil(this.totalCount() / this.pageSize()));
  hasPrevious = computed(() => this.pageNumber() > 1);
  hasNext = computed(() => this.pageNumber() < this.totalPages());

  searchBy = signal('');
  sortBy = signal<string>('');
  sortDirection = signal<'asc' | 'desc'>('asc');
  SeatingCapacity = signal('');
  vehicleCategoryId = signal<string>('');
  RentalCompanyId = signal<string>('');
  loading = signal(false);
  vehicle = signal<AvailableVehicle[]>([]);
  vehicleCategory = signal<VehicleCategory[]>([]);
  RentalCompany = signal<RentalCompany[]>([]);
  currentAvailableVehicle = signal<AvailableVehicle | null>(null);
  showBookVehicle = signal(false);
  fromDailyRate = signal('');
  toDailyRate = signal('');

  isAsc = signal(true);

  customerService = inject(CustomerService);
  utilityService = inject(UtitlityService);

  column: tableColumn<AvailableVehicle>[] = [
    {
      key: 'id',
      label: 'Vehicle Id',
      sortable: true,
      sortKey: 'id',
    },
    {
      key: 'makeAndModel',
      label: 'Make and model',
      sortable: true,
      sortKey: 'makeAndModel',
    },
    {
      key: 'licensePlate',
      label: 'License Plate',
      sortable: true,
      sortKey: 'licensePlate',
      format: (value) => this.utilityService.convertToCapital(value as string),
    },
    {
      key: 'registrationNumber',
      label: 'Registration Number',
      sortable: true,
      sortKey: 'registrationNumber',
      format: (value) => this.utilityService.convertToCapital(value as string),
    },
    {
      key: 'yearofManufacture',
      label: 'Year Of Manufacture',
      sortable: true,
      sortKey: 'yearofManufacture',
    },
    {
      key: 'seatingCapacity',
      label: 'Seating Capacity',
      sortable: true,
      sortKey: 'seatingCapacity',
    },
    {
      key: 'dailyRentalRate',
      label: 'Daily Rental Rate',
      sortable: true,
      sortKey: 'dailyRentalRate',
    },
    {
      key: 'companyName',
      label: 'Company Name',
      sortable: true,
      sortKey: 'companyName',
    },
    {
      key: 'categoryName',
      label: 'Category Name',
      sortable: true,
      sortKey: 'categoryName',
    },
    {
      key: 'mileage',
      label: 'Mileage',
      sortable: true,
      sortKey: 'mileage',
    },
    {
      key: 'id',
      label: 'action',
      type: 'actions',
    },
  ];

  action: tableAction<AvailableVehicle>[] = [
    {
      label: 'Reserve',
      action: 'Reserve',
      class:
        'bg-blue-500 text-white font-bold px-6 py-4 rounded-xl cursor-pointer hover: bg-blue-700 transition',
    },
  ];

  onTableAction(event: { action: string; row: AvailableVehicle }) {
    if (event.action == 'Reserve') {
      this.currentAvailableVehicle.set(event.row);
      this.showBookVehicle.set(true);
    }
  }

  ngOnInit(): void {
    this.loadVehicleCategory();
    this.loadRentalCompany();
    this.loadVehicle();

    const today = new Date();
    const year = today.getFullYear();
    const mnth = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    this.minDateString = `${year}-${mnth}-${day}`;
  }

  minDateString: string = '';

  loadVehicle() {
    this.customerService
      .getPagedAvailableVehicle({
        PageNumber: this.pageNumber(),
        PageSize: this.pageSize(),
        SortBy: this.sortBy(),
        SortDirection: this.sortDirection(),
        SearchBy: this.searchBy(),
        VehicleCategoryId: this.vehicleCategoryId(),
        SeatingCapacity: this.SeatingCapacity(),
        RentalCompanyId: this.RentalCompanyId(),
        FromDailyRentalRate: +this.fromDailyRate(),
        ToDailyRentalRate: +this.toDailyRate(),
      })
      .subscribe({
        next: (res) => {
          this.vehicle.set(res.items);
          this.pageNumber.set(res.pageNumber);
          this.totalCount.set(res.totalCount);

          console.log(res);
        },
        error: (err) => {
          console.log(err);
        },
      });
  }

  loadVehicleCategory() {
    this.customerService.getVehicleCategory().subscribe({
      next: (res) => {
        console.log(res);
        this.vehicleCategory.set(res);
      },
    });
  }

  loadRentalCompany() {
    this.customerService.getRentalCompany().subscribe({
      next: (res) => {
        this.RentalCompany.set(res);
      },
    });
  }

  seatings = signal(Array.from({ length: 20 }, (_, index) => 20 - index));

  onSortBy(value: string) {
    if (this.sortBy() == value) this.isAsc.update((x) => !x);
    else {
      this.isAsc.set(true);
      this.sortBy.set(value);
    }
    this.sortDirection.set(this.isAsc() == true ? 'asc' : 'desc');
    this.pageNumber.set(1);
    this.loadVehicle();
  }

  onSearchBy(value: string) {
    this.searchBy.set(value);
    this.pageNumber.set(1);
    this.loadVehicle();
  }

  onVehicleCategoryId(value: string) {
    this.vehicleCategoryId.set(value);
    this.pageNumber.set(1);
    this.loadVehicle();
  }

  onSeatingCapacityChange(value: string) {
    this.SeatingCapacity.set(value);
    this.pageNumber.set(1);
    this.loadVehicle();
  }

  onRentalCompanyId(value: string) {
    this.RentalCompanyId.set(value);
    this.pageNumber.set(1);
    this.loadVehicle();
  }

  onFromDailyRateChange(value: string) {
    this.fromDailyRate.set(value);
    console.log(this.fromDailyRate());
  }
  onToDailyRateChange(value: string) {
    this.toDailyRate.set(value);
    console.log(this.toDailyRate());
  }
  applyDailyRateFilter() {
    if (this.fromDailyRate() && this.toDailyRate()) {
      if (Number(this.fromDailyRate()) > Number(this.toDailyRate())) {
        alert(`to Daily rental rate should be greater than from daily rental rate`);
        this.fromDailyRate.set("");
        this.toDailyRate.set("");
        return;
      }
    }
    this.loadVehicle();
  }

  closeBookVehicle() {
    this.currentAvailableVehicle.set(null);
    this.showBookVehicle.set(false);
  }

  previousPage() {
    if (this.hasPrevious()) {
      this.pageNumber.update((x) => x - 1);
      this.loadVehicle();
    }
  }
  nextPage() {
    if (this.hasNext()) {
      this.pageNumber.update((x) => x + 1);
      this.loadVehicle();
    }
  }
}
