import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { AdminService } from '../../../core/services/admin-service';
import { RentalCompany } from '../../../Enviroment/model';
import {
  tableColumn,
  GenericTable,
  tableAction,
} from '../../../shared/generic-table/generic-table';
import { AdminAddCompany } from '../admin-add-company/admin-add-company';
import { AdminUpdateCompany } from '../admin-update-company/admin-update-company';
import { DeleteModal } from '../../../shared/delete-modal/delete-modal';
import { UtitlityService } from '../../../core/services/utitlity-service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admin-rental-company',
  imports: [GenericTable, AdminAddCompany, AdminUpdateCompany, DeleteModal],
  templateUrl: './admin-rental-company.html',
  styleUrl: './admin-rental-company.css',
})
export class AdminRentalCompany implements OnInit {
  pageNumber = signal(1);
  pageSize = signal(5);
  totalCount = signal(0);
  totalPages = computed(() => Math.ceil(this.totalCount() / this.pageSize()));
  hasPrevious = computed(() => this.pageNumber() > 1);
  hasNext = computed(() => this.pageNumber() < this.totalPages());

  searchBy = signal('');
  sortBy = signal<string>('');
  sortDirection = signal<'asc' | 'desc'>('asc');
  companyType = signal<string>('');
  companyStatus = signal<string>('');
  loading = signal(false);
  rentalCompany = signal<RentalCompany[]>([]);
  currentRentalCompany = signal<RentalCompany | null>(null);

  isAsc = signal(true);
  adminService = inject(AdminService);
  utilityService = inject(UtitlityService);
  toastr = inject(ToastrService);

  showAddCompanyModal = signal(false);
  showUpdateCompanyModal = signal(false);
  showDeleteCompanyModal = signal(false);

  column: tableColumn<RentalCompany>[] = [
    {
      key: 'id',
      label: 'Company Code',
      sortable: true,
      sortKey: 'id',
    },
    {
      key: 'companyName',
      label: 'Company Name',
      sortable: true,
      sortKey: 'companyName',
    },
    {
      key: 'companyType',
      label: 'Company Type',
      sortable: true,
      sortKey: 'companyType',
      format: (value) => this.utilityService.getCompanyType(value as number),
    },
    {
      key: 'ownerName',
      label: 'Owner Name',
      sortable: true,
      sortKey: 'ownerName',
    },
    {
      key: 'phoneNumber',
      label: 'Phone Number',
      sortable: true,
      sortKey: 'phoneNumber',
    },
    {
      key: 'headquartersLocation',
      label: 'Headquarters Location',
      sortable: true,
      sortKey: 'headquartersLocation',
    },
    {
      key: 'licenseNumber',
      label: 'License Number',
      sortable: true,
      sortKey: 'licenseNumber',
      format: (value) => this.utilityService.convertToCapital(value as string),
    },
    {
      key: 'status',
      label: 'Company Status',
      sortable: true,
      sortKey: 'companyStatus',
      format: (value) => this.utilityService.getCompanyStatus(value as number),
    },
    {
      key: 'id',
      label: 'Actions',
      type: 'actions',
    },
  ];

  action: tableAction<RentalCompany>[] = [
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

  onTableAction(event: { action: string; row: RentalCompany }) {
    if (event.action == 'Update') {
      this.currentRentalCompany.set(event.row);
      this.openUpdateCompanyModal();
    } else if (event.action == 'Delete') {
      this.currentRentalCompany.set(event.row);
      this.showDeleteCompanyModal.set(true);
    }
  }
  ngOnInit(): void {
    this.loadRentalCompany();
  }

  loadRentalCompany() {
    console.log(this.sortBy());
    console.log(this.sortDirection());
    this.adminService
      .getPagedRentalCompany({
        PageNumber: this.pageNumber(),
        PageSize: this.pageSize(),
        SortBy: this.sortBy(),
        SortDirection: this.sortDirection(),
        SearchBy: this.searchBy(),
        CompanyType: this.companyType(),
        CompanyStatus: this.companyStatus(),
      })
      .subscribe({
        next: (res) => {
          this.rentalCompany.set(res.items);
          this.pageNumber.set(res.pageNumber);
          this.totalCount.set(res.totalCount);

          console.log(res);
        },
        error: (err) => {
          console.log(err);
        },
      });
  }

  deleteRentalCompany() {
    this.adminService.deleteCompany(this.currentRentalCompany()?.id!).subscribe({
      next: (res) => {
        this.closeDeleteCompanyModal();
        if (
          this.pageNumber() == this.totalPages() &&
          this.totalCount() % this.pageSize() == 1 &&
          this.pageNumber() > 1
        ) {
          this.pageNumber.update((x) => x - 1);
        }
        this.loadRentalCompany();
        this.toastr.success('rental company deleted');
      },
      error: (err) => {
        this.closeDeleteCompanyModal();
        this.toastr.error(err.error?.detail || 'something went wrong');
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
    this.loadRentalCompany();
  }

  onSearchBy(value: string) {
    this.searchBy.set(value);
    this.pageNumber.set(1);
    this.loadRentalCompany();
  }
  onCompanyStatusChange(value: string) {
    this.companyStatus.set(value);
    this.pageNumber.set(1);
    this.loadRentalCompany();
  }

  onCompanyTypeChange(value: string) {
    this.companyType.set(value);
    this.pageNumber.set(1);
    this.loadRentalCompany();
  }

  previousPage() {
    if (this.hasPrevious()) {
      this.pageNumber.update((x) => x - 1);
      this.loadRentalCompany();
    }
  }
  nextPage() {
    if (this.hasNext()) {
      this.pageNumber.update((x) => x + 1);
      this.loadRentalCompany();
    }
  }

  openAddCompanyModal() {
    this.showAddCompanyModal.set(true);
  }
  closeAddCompanyModal() {
    this.loadRentalCompany();
    this.showAddCompanyModal.set(false);
  }
  openUpdateCompanyModal() {
    this.showUpdateCompanyModal.set(true);
  }
  closeUpdateCompanyModal() {
    this.loadRentalCompany();
    this.showUpdateCompanyModal.set(false);
  }

  closeDeleteCompanyModal() {
    this.loadRentalCompany();
    this.currentRentalCompany.set(null);
    this.showDeleteCompanyModal.set(false);
  }
}
