import { Component, computed, inject, signal } from '@angular/core';
import { AdminService } from '../../../core/services/admin-service';
import {
  GenericTable,
  tableAction,
  tableColumn,
} from '../../../shared/generic-table/generic-table';
import { RentalCompany, Vehicle, VehicleCategory } from '../../../Enviroment/model';
import { AdminAddVehicle } from '../admin-add-vehicle/admin-add-vehicle';
import { AdminUpdateCompany } from '../admin-update-company/admin-update-company';
import { AdminUpdateVehicle } from '../admin-update-vehicle/admin-update-vehicle';
import { DeleteModal } from '../../../shared/delete-modal/delete-modal';
import { UtitlityService } from '../../../core/services/utitlity-service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admin-vehicle',
  imports: [GenericTable, AdminAddVehicle, AdminUpdateVehicle, DeleteModal],
  templateUrl: './admin-vehicle.html',
  styleUrl: './admin-vehicle.css',
})
export class AdminVehicle {
  pageNumber = signal(1);
  pageSize = signal(5);
  totalCount = signal(0);
  totalPages = computed(() => Math.ceil(this.totalCount() / this.pageSize()));
  hasPrevious = computed(() => this.pageNumber() > 1);
  hasNext = computed(() => this.pageNumber() < this.totalPages());

  searchBy = signal('');
  sortBy = signal<string>('');
  sortDirection = signal<'asc' | 'desc'>('asc');
  vehicleCategoryId = signal<string>('');
  vehicleAvailabilityStatus = signal<string>('');
  loading = signal(false);
  vehicle = signal<Vehicle[]>([]);
  vehicleCategory = signal<VehicleCategory[]>([]);
  rentalCompany = signal<RentalCompany[]>([]);
  currentVehicle = signal<Vehicle | null>(null);

  showAddVehicleModal = signal(false);
  showUpdateVehicleModal = signal(false);
  showDeleteVehicleModal = signal(false);

  isAsc = signal(true);

  adminService = inject(AdminService);
  utilityService = inject(UtitlityService);
  toastr = inject(ToastrService);

  column: tableColumn<Vehicle>[] = [
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
      key: 'vehicleAvailabilityStatus',
      label: 'Vehicle  Status',
      sortable: true,
      sortKey: 'vehicleAvailabilityStatus',
      format: (value) => this.utilityService.getVehicleAvailabilityStatus(value as number),
    },
    {
      key: 'mileage',
      label: 'Mileage',
      sortable: true,
      sortKey: 'mileage',
    },
    {
      key: 'rentalCompanyName',
      label: 'Company Name',
      sortable: true,
      sortKey: 'companyName',
    },
    {
      key: 'id',
      label: 'actions',
      type: 'actions',
    },
  ];

  action: tableAction<Vehicle>[] = [
    {
      label: 'Update',
      action: 'Update',
      class:
        'bg-blue-500 text-white font-bold px-6 py-4 rounded-xl cursor-pointer hover: bg-blue-700 transition',
    },
    {
      label: 'Delete',
      action: 'Delete',
      class:
        'bg-red-500 text-white font-bold px-6 py-4 rounded-xl cursor-pointer hover: bg-red-700 transition',
    },
  ];

  onTableAction(event: { action: string; row: Vehicle }) {
    if (event.action == 'Update') {
      this.currentVehicle.set(event.row);
      this.openUpdateVehicleModal();
    } else if (event.action == 'Delete') {
      this.currentVehicle.set(event.row);
      this.showDeleteVehicleModal.set(true);
    }
  }

  ngOnInit(): void {
    this.loadVehicle();
    this.loadVehicleCategory();
    this.loadCompany();
  }

  loadVehicle() {
    this.adminService
      .getPagedVehicle({
        PageNumber: this.pageNumber(),
        PageSize: this.pageSize(),
        SortBy: this.sortBy(),
        SortDirection: this.sortDirection(),
        SearchBy: this.searchBy(),
        VehicleCategoryId: this.vehicleCategoryId(),
        VehicleAvailabilityStatus: this.vehicleAvailabilityStatus(),
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

  loadCompany() {
    this.adminService.getRentalCompany().subscribe({
      next: (res) => {
        this.rentalCompany.set(res);
      },
    });
  }

  loadVehicleCategory() {
    this.adminService.getVehicleCategory().subscribe({
      next: (res) => {
        console.log(res);
        this.vehicleCategory.set(res);
      },
    });
  }

  deleteVehicle() {
    this.adminService.deleteVehicle(this.currentVehicle()?.id!).subscribe({
      next: (res) => {
        if (
          this.pageNumber() == this.totalPages() &&
          this.totalCount() % this.pageSize() == 1 &&
          this.pageNumber() > 1
        ) {
          this.pageNumber.update((x) => x - 1);
        }
        this.closeDeleteVehicleModal();
        this.toastr.success('Vehicle deleted');
      },
      error: (err) => {
        this.closeDeleteVehicleModal();
        this.toastr.error(err.error?.detail);
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
    this.loadVehicle();
  }

  onSearchBy(value: string) {
    this.searchBy.set(value);
    this.pageNumber.set(1);
    this.loadVehicle();
  }

  onVehicleAvailabilityStatusChange(value: string) {
    this.vehicleAvailabilityStatus.set(value);
    this.pageNumber.set(1);
    this.loadVehicle();
  }

  onVehicleCategoryId(value: string) {
    this.vehicleCategoryId.set(value);
    console.log(this.vehicleCategoryId());
    console.log(this.vehicleAvailabilityStatus());
    this.pageNumber.set(1);
    this.loadVehicle();
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

  openAddVehicleModal() {
    this.showAddVehicleModal.set(true);
  }

  closeAddVehicleModal() {
    this.loadVehicle();
    this.showAddVehicleModal.set(false);
  }

  openUpdateVehicleModal() {
    this.showUpdateVehicleModal.set(true);
  }
  closeUpdateVehicleModal() {
    this.loadVehicle();
    this.showUpdateVehicleModal.set(false);
  }

  closeDeleteVehicleModal() {
    this.loadVehicle();
    this.currentVehicle.set(null);
    this.showDeleteVehicleModal.set(false);
  }
}
