import * as _ from 'lodash';

class Common {

  private browserTimezoneOffSetValue: number;
  defaultGuid = '00000000-0000-0000-0000-000000000000';

  get browserTimeZoneOffset(): number {
    if (this.isNil(this.browserTimezoneOffSetValue)) {
      this.browserTimezoneOffSetValue = new Date().getTimezoneOffset() * -1;
    }

    return this.browserTimezoneOffSetValue;
  }

  isNil(value: any): boolean {
    return _.isNil(value);
  }

  isStrNullOrWhiteSpace(value: string): boolean {
    if (this.isNil(value)) { return true; }
    if (value.trim().length === 0) { return true; }
    return false;
  }


  YYYYMMDDToDate(dateStr: string): Date {
    if (common.isNil(dateStr) || dateStr.length === 0) {
      return undefined;
    }
    const year = dateStr.substring(0, 4);
    const month = dateStr.substring(4, 6);
    const day = dateStr.substring(6, 8);

    return new Date(+year, +month - 1, +day);
  }


  dateToYYYYMMDD(value: Date): string {
    if (this.isNil(value)) {
      return undefined;
    }
    const y = value.getFullYear();
    const m = value.getMonth() + 1;
    const d = value.getDate();
    return '' + y + (m < 10 ? '0' : '') + m + (d < 10 ? '0' : '') + d;
  }


  isDate(obj: any): boolean {
    return _.isDate(obj);
  }

  isObject(obj: any): boolean {
    return _.isObject(obj);
  }

  isArray(obj: any): boolean {
    return _.isArray(obj);
  }

  isNumber(obj: any): boolean {
    return _.isNumber(obj);
  }

  isString(obj: any): boolean {
    return _.isString(obj);
  }

  /** Object deep clone */
  cloneDeep(value: any) {
    return _.cloneDeep(value);
  }

  /** JSON stringify clone */
  clone<T>(obj: T): T {
    return <T>JSON.parse(JSON.stringify(obj));
  }

  arrayDeepClone(array: any[]) {
    return _.map(array, _.clone);
  }
}

export const common = new Common();
