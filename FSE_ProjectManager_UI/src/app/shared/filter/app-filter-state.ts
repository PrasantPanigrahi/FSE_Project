import { common } from '../../core/common';
import { FilterLogic } from './app-filter-enum';
import { appFilterHelper } from './app-filter-helper';
import { CompositeFilterDescriptor } from './models/composite-filter-descriptor';
import { FilterState } from './models/filter-state';
import { SortDescriptor } from './models/sort-descriptor';

export class AppFilterState {

  /**
     * The number of records to be skipped by the pager.
     */
  skip: number;

  /**
   * The number of records to take.
   * Default to 30.
   */
  take: number;

  /**
   * The descriptors used for sorting.
   */
  private sort?: Array<SortDescriptor>;
  /**
   * The descriptors used for filtering.
   */
  private filter?: CompositeFilterDescriptor;

  /**
   * The descriptors used for grouping.
   * Not in used.
   */
  readonly group?: Array<any>;

  private readonly logicAnd = 'and';

  constructor() {
    this.skip = 0;
    this.take = 15;
  }


  init(filterState: FilterState) {
    if (!common.isNil(filterState)) {
      this.skip = filterState.skip;
      this.take = filterState.take;

      if (!common.isNil(filterState.sort)) {
        filterState.sort.forEach(s => this.addSort(s.field, s.dir));
      }

      if (!common.isNil(filterState.filter)) {
        appFilterHelper
          .compositeFilterDescriptorFlatten(filterState.filter)
          .forEach(f => this.addFilter(f.field.toString(), f.value, f.operator.toString()));
      }
    }
  }

  export(): FilterState {
    return {
      skip: this.skip,
      take: this.take,
      sort: this.sort,
      filter: this.filter
    };
  }

  /** To add a sort descriptor */
  addSort(field: string, direction: string) {
    if (direction !== 'asc' && direction !== 'desc') {
      throw new Error('either "asc" or "desc" is acceptable {direction} ');
    }

    const root = this.sort || [];
    const sortFound = appFilterHelper.searchSortDescriptor(root, field);

    if (!sortFound) {
      root.push({
        field: field,
        dir: direction
      });
    } else {
      sortFound.dir = direction;
    }
    this.sort = root;
  }

  /** To add a filter descriptor.
   * Warning! Multiple filters with same {field} name will be assign with same {operator} and {value} */
  addFilter(field: string, filterValue: any, filterLogic: string | FilterLogic) {
    const root = this.filter || { logic: 'and', filters: [] };

    const filtersFound = appFilterHelper.searchFilterDescriptor(root, field);

    if (common.isNil(filterValue) || filterValue.toString().length === 0) {
      appFilterHelper.removeFilterDescriptor(root, field);
    } else {
      const operator = common.isNumber(filterLogic)
        ? appFilterHelper.filterLogicConvert(<FilterLogic>filterLogic) : filterLogic.toString();

      if (filtersFound.length === 0) {
        root.filters.push({
          field: field,
          operator: operator,
          value: filterValue
        });
      } else {
        // set all found filters with same {operator} and {value}
        filtersFound.forEach(f => {
          f.operator = operator;
          f.value = filterValue;
        });
      }
      this.filter = root;
    }
  }
}
