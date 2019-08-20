import { Injectable } from '@angular/core';

import { common } from '../../core/common';
import { CompositeFilterDescriptor } from './models/composite-filter-descriptor';
import { FilterOperatorType } from './models/filter-enums';
import { FilterState } from './models/filter-state';
import { SortDescriptor } from './models/sort-descriptor';

@Injectable({
  providedIn: 'root'
})
export class TableFilterStateService {

  /** No of rows per page */
  public rows = 15;


  public rowsPerPageOptions = [15, 50];

  /** The number of records to be skipped by the pager. */
  private skip: number;

  /** The number of records to take. Default 50 */
  private take: number;

  /** The descriptors used for sorting */
  private sort: SortDescriptor[];

  /** The descriptors used for filtering */
  private filter?: CompositeFilterDescriptor;

  constructor() {
    this.skip = 0;
    this.take = this.rows;
  }

  /** Go to page one */
  goToPageOne() {
    this.skip = 0;
    this.take = this.rows;
  }

  setPageCount(rows: number) {
    this.rows = rows;
  }

  onSort(field: string, order: number) {
    if (!common.isStrNullOrWhiteSpace(field) && common.isNumber(order)) {
      this.sort = [{ field, dir: order > 0 ? 'asc' : 'desc' }];
    } else {
      this.sort = undefined;
    }
  }

  onPaginate(skip: number, take: number) {
    this.skip = skip;
    this.take = take;
  }

  removeFilter(field: string, operator: string) {
    this.goToPageOne();

    if (!common.isNil(this.filter) && !common.isNil(this.filter.filters)) {
      let index = -1;
      this.filter.filters.forEach((f, i) => {
        if (f.field.toString().trim().toUpperCase() === field.trim().toUpperCase()
          && f.operator.toString().trim().toUpperCase() === operator.trim().toUpperCase()) {
          index = i;
        }
      });

      if (index > -1) {
        this.filter.filters.splice(index, 1);
      }
    }
  }

  onFilter(field: string, value: any, operator: FilterOperatorType) {

    this.goToPageOne();

    if (common.isNil(this.filter)) {
      this.filter = { logic: 'and', filters: [] };
    }

    const found = this.filter.filters
      .filter(f => f.field.toString().trim().toUpperCase() === field.trim().toUpperCase()
        && f.operator.toString().trim().toUpperCase() === operator.trim().toUpperCase());

    if (found.length > 0 && !common.isNil(found[0])) {
      found[0].value = value;
    } else {
      this.filter.filters.push({
        field,
        value,
        operator
      });
    }
  }

  extract(): FilterState {
    return {
      skip: this.skip,
      take: this.take,
      filter: this.filter,
      sort: this.sort,
      timeZoneOffset: common.browserTimeZoneOffset
    };
  }
}
