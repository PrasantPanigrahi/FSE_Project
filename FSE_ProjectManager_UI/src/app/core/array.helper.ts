import * as _ from 'lodash';

import { common } from './common';

export enum sortType {
  asc = 0,
  desc = 1
}


class ArrayHelper {

  /** Sort array */
  sort(array: any[], field: string, type: sortType = sortType.asc): any[] {
    return _.orderBy(array, [field], [(type === sortType.asc ? 'asc' : 'desc')]);
  }

  /** Find the max item from array */
  max(array: any[], field: string): number {
    return Math.max.apply(Math, array.map(function (i) { return i[field]; }));
  }

  /** Iterates over elements of collection, returning the first element predicate returns truthy for. */
  firstOrDefault(array: any[], predicate: Function) {
    return _.find(array, ['invalid', true]);
  }
}

export const arrayHelper = new ArrayHelper();
