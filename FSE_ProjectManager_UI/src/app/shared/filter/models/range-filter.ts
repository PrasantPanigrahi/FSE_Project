export interface RangeFilter<T> {
  from: T;
  to: T;
  /** Time zone offset (minutes) with client browser.
   * Only applicable to DateTime
  */
  timeZoneOffset?: number;
}
