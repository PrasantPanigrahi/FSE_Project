import * as _ from 'lodash';

import { FilterLogic } from './app-filter-enum';
import { CompositeFilterDescriptor } from './models/composite-filter-descriptor';
import { FilterDescriptor } from './models/filter-descriptor';
import { SortDescriptor } from './models/sort-descriptor';

class AppFilterHelper {
  /** Flatten out a CompositeFilterDescriptor to FilterDescriptor[]  */
  compositeFilterDescriptorFlatten(compositeFilter: CompositeFilterDescriptor): FilterDescriptor[] {

    /**
     * https://www.tutorialspoint.com/typescript/typescript_array_reduce.htm
     * reduce() method applies a function simultaneously against two values
     * of the array (from left-to-right) as to reduce it to a single value.
     *
     * var total = [0, 1, 2, 3].reduce(function(a, b){ return a + b; });
     * total is : 6
     *
     *
     * https://www.tutorialspoint.com/typescript/typescript_array_concat.htm
     * concat() method returns a new array comprised of this array joined with two or more arrays.
     *
     * var alpha = ["a", "b", "c"];
     * var numeric = [1, 2, 3];     *
     * var alphaNumeric = alpha.concat(numeric);
     * result: alphaNumeric : a,b,c,1,2,3
     */

    // filter: CompositeFilterDescriptor
    const filters = (<any>(compositeFilter || {})).filters;
    if (filters) {
      return filters.reduce((acc, curr) => acc.concat(curr.filters ? this.compositeFilterDescriptorFlatten(curr) : [curr]), []);
    }
    return [];
  }

  /** Search and return found [FilterDescriptor](s) by field name */
  searchFilterDescriptor(compositeFilter: CompositeFilterDescriptor, fieldName: string): FilterDescriptor[] {
    return this.compositeFilterDescriptorFlatten(compositeFilter)
      .filter((x: FilterDescriptor) => x.field === fieldName);
  }

  /** Remove [FilterDescriptor](s) by field name */
  removeFilterDescriptor(compositeFilter: CompositeFilterDescriptor, fieldName: string) {
    const filters = (<any>(compositeFilter || {})).filters;

    if (filters) {
      if (filters) {
        this.removeFilterDescriptor(filters, fieldName);
      } else {
        _.remove(filters, (f: FilterDescriptor) => {
          return f.field === fieldName;
        });
      }
    }
  }

  /** Search and return first found [SortDescriptor] by field name */
  searchSortDescriptor(sortDescriptors: SortDescriptor[], fieldName: string): SortDescriptor {
    if (sortDescriptors) {
      return sortDescriptors.filter(x => x.field === fieldName)[0];
    } else {
      return undefined;
    }
  }

  /** Convert enum FilterLogic to str */
  filterLogicConvert(filterLogic: FilterLogic): string {
    switch (filterLogic) {

      case FilterLogic.equalTo: return 'eq'; break;
      // /** "neq" */
      // notEqualTo,
      // /** "isnull" */
      // isEqualToNull,
      // /** "isnotnull" */
      // isNotEqualToNull,
      case FilterLogic.greaterThanOrEqualTo: return 'gte'; break;

      /// The following operators are supported for string fields only

      // /** "startswith" */
      // startsWith,
      // /** "endswith" */
      // endsWith,
      case FilterLogic.contains: return 'contains'; break;
      // /** "doesnotcontain" */
      // doesNotContain,
      // /** "isempty" */
      // isEmpty,
      // /** "isnotempty" */
      // isNotEmpty
    }
  }
}

export const appFilterHelper = new AppFilterHelper();
