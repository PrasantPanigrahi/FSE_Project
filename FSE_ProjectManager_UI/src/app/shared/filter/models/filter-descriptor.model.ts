import { FilterOperatorType } from './filter-enums';

export interface FilterDescriptorModel<T> {
  field: string;
  value: T;
  operator: FilterOperatorType;
}
