import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/internal/operators/debounceTime';

import { common } from '../../core/common';
import { FilterList } from './filter-list';
import { FilterDescriptorModel } from './models/filter-descriptor.model';
import { FilterOperatorType } from './models/filter-enums';
import { RangeFilter } from './models/range-filter';
import { TableFilterStateService } from './table-filter-state.service';

export class FilterListHelper {

  private debounceFilterSubject: Subject<FilterDescriptorModel<any>> = new Subject();

  /** Filter text filed debounce */
  textFilterDebounce = 500;
  dateCreatedRangeFilter: RangeFilter<Date> = { from: undefined, to: undefined };
  dateRangeFilter: RangeFilter<Date> = { from: undefined, to: undefined };

  constructor(private component: FilterList,
    private filterStateService: TableFilterStateService) {
    /** https://stackoverflow.com/questions/42761163/angular-2-debouncing-a-keyup-event */
    this.debounceFilterSubject
      .pipe(debounceTime(this.textFilterDebounce))
      .subscribe((filterDescriptor: FilterDescriptorModel<any>) => {
        this.filterStateService.onFilter(filterDescriptor.field, filterDescriptor.value, filterDescriptor.operator);
        this.component.refresh();
      });
  }

  dateCreatedFilterInit(backdatedDays: number = 7) {
    const today = new Date();
    const from = new Date();
    from.setDate(today.getDate() - backdatedDays);
    this.dateCreatedRangeFilter = {
      from: from,
      to: today
    };
    this.onRange(this.dateCreatedRangeFilter, 'dateCreated', false);
  }

  /** On key-up debounce */
  onKeyup(value: string, field: string, operator: FilterOperatorType = FilterOperatorType.contains) {
    const filterDescriptor: FilterDescriptorModel<string> = {
      field,
      value,
      operator: operator
    };

    this.debounceFilterSubject.next(filterDescriptor);
  }

  onChange(value: any, field: string, operator: FilterOperatorType = FilterOperatorType.equalTo) {
    if (common.isNil(value)
      || (common.isArray(value) && value.length === 0)) {
      this.filterStateService.removeFilter(field, operator);
    } else {
      this.filterStateService.onFilter(field, value, operator);
    }
    this.component.refresh();
  }

  onRange(value: RangeFilter<any>, field: string, refresh: boolean = true) {
    if (!common.isNil(value.from) && !common.isNil(value.to)) {

      let filterDescriptor: FilterDescriptorModel<RangeFilter<any>> = {
        field,
        value,
        operator: FilterOperatorType.range
      };

      switch (field.toLocaleLowerCase()) {
        case 'datecreated':
          if (common.isDate(value.from)
            && common.isDate(value.to)) {
            filterDescriptor = {
              field,
              value: {
                from: common.dateToYYYYMMDD(value.from),
                to: common.dateToYYYYMMDD(value.to),
                timeZoneOffset: common.browserTimeZoneOffset,
              },
              operator: FilterOperatorType.range
            };
          } else {
            filterDescriptor = undefined;
          }
          break;
      }

      this.filterStateService.onFilter(filterDescriptor.field, filterDescriptor.value, filterDescriptor.operator);
      if (refresh) { this.component.refresh(); }
    } else {
      this.filterStateService.removeFilter(field, FilterOperatorType.range);
      if (refresh) { this.component.refresh(); }
    }
  }

}
