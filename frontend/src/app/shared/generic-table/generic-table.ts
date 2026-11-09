import { Component, input, output } from '@angular/core';

export interface tableColumn<T> {
  key: keyof T;
  label: string;
  sortable?: boolean;
  sortKey?: string;
  type?: 'text' | 'actions';
  format?: (value: T[keyof T]) => string;
}

export interface tableAction<T> {
  label: string;
  action: string;
  disabled?: (row: T) => boolean;
  showIf?: (row: T) => boolean;
  class?: string;
}

@Component({
  selector: 'app-generic-table',
  imports: [],
  templateUrl: './generic-table.html',
  styleUrl: './generic-table.css',
})
export class GenericTable<T> {
  data = input.required<T[]>();
  columns = input.required<tableColumn<T>[]>();
  actions = input<tableAction<T>[]>();

  sortBy = input<string>();
  sortDirection = input<'desc' | 'asc'>('asc');
  sort = output<string>();
  action = output<{ action: string; row: T }>();

  onSort(column: tableColumn<T>) {
    if(column.sortable){
      this.sort.emit(column.sortKey as string);
    }
  }

  onAction(action: string, row: T) {
    this.action.emit({action, row});
  }

  getCellValue(column: tableColumn<T>, row: T) {
    const value = row[column.key];

    if (column.format) {
     return column.format(value)
    }
    return value;
  }
}
