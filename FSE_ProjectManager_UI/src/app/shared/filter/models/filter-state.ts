import { CompositeFilterDescriptor } from './composite-filter-descriptor';
import { SortDescriptor } from './sort-descriptor';

/**
 * The state of the data operations applied to the Grid component.
 */
export interface FilterState {
  /**
   * The number of records to be skipped by the pager.
   */
  skip?: number;
  /**
   * The number of records to take.
   */
  take?: number;
  /**
   * The descriptors used for sorting.
   */
  sort?: Array<SortDescriptor>;
  /**
   * The descriptors used for filtering.
   */
  filter?: CompositeFilterDescriptor;

  /** Time zone offset (minutes) with client browser.
   * Only applicable to DateTime
  */
  timeZoneOffset?: number;
}
