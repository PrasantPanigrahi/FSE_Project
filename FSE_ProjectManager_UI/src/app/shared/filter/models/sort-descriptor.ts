/**
 * The sort descriptor used by the `orderBy` method.
 *
 * It has the following properties:
 */
export interface SortDescriptor {
  /**
   * The field that is sorted.
   */
  field: string;
  /**
   * The sort direction.
   * If no direction is set, the descriptor will be skipped during processing.
   *
   * The available values are:
   * - `asc`
   * - `desc`
   */
  dir?: 'asc' | 'desc';
}
