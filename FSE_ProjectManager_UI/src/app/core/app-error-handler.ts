import { ConfirmationService } from 'primeng/api';
import { common } from 'src/app/core/common';
import { environment } from 'src/environments/environment';

import { ErrorHandler, Injectable } from '@angular/core';

@Injectable()
export class AppErrorHandler implements ErrorHandler {

  constructor(private confirmationService: ConfirmationService) { }

  handleError(error: any) {

    // todo: consider https://rollbar.com/pricing/
    // https://scotch.io/bar-talk/error-handling-with-angular-6-tips-and-best-practices192#toc-tracking-angular-errors-with-rollbar

    // todo: or consider https://stackoverflow.com/questions/50970446/http-error-handling-in-angular-6

    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);

      // todo: log error to backend api

    } else if (error.error instanceof ProgressEvent) {
      console.error(
        `ProgressEvent` +
        `error was: ${JSON.stringify(error)}`);

    } else if (error.status) {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned status code [${error.status}], ` +
        `error [${JSON.stringify(error.error)}], ` +
        `message was: ${error.message}`);

      if (error.status === 401) {
        window.location.href = '';
      }

      let errMsg = '';
      /** API ModelState validations error pattern */
      if (!common.isNil(error.error.errors) ||
        (!common.isNil(error.error.title) && error.error.title.indexOf('One or more validation errors occurred') > 0)
      ) {
        errMsg = this.parseModalStateValidationErrorObject(error.error);
      } else if (common.isString(error.error)) {
        errMsg = error.error;
      } else if (common.isString(error.error.Message)) {
        errMsg = error.error.Message;
      } else {
        errMsg = JSON.stringify(error.error);
      }

      alert(errMsg);

    } else {
      console.error(error);
      if (!common.isNil(error.message)) {
        alert(error.message);
      }
    }

    if (!common.isNil(error.message)) {
      if (!environment.production) {

        // this.confirmationService.confirm({
        //   message: error.message,
        //   header: 'Confirmation',
        //   icon: 'pi pi-exclamation-triangle',
        //   accept: () => {
        //   }
        // });
      }

    }

    /** https://stackoverflow.com/questions/44356040/angular-global-error-handler-working-only-once
     *
     * we should not rethrow the error in errorhandler
     *
     * BELOW IS THE ORIGINAL CODE:
     * // IMPORTANT: Rethrow the error otherwise it gets swallowed
     * // throw error;
    */
  }

  parseModalStateValidationErrorObject(error: object): string {
    let errMsg = '';
    for (const key in error) {
      if (error.hasOwnProperty(key)) {
        const value = error[key];

        /** API ModelState validations error pattern
         * { "errors": { "Code": ["Code not provided"], "Name": ["Name not provided"], "FinalCost": ["Cost must be between 1 and 999999999"], "CategoryId": ["Category not provided"] }, "title": "One or more validation errors occurred.", "status": 400, "traceId": "0HLKVN3B0RUHG:00000008" }
         */
        // if it's an array
        // then it's highly possibility it's an API ModelState validation error pattern
        if (common.isArray(value)) {
          value.forEach(msg => {
            errMsg += msg;
            errMsg += '\r\n';
          });
        } else if (common.isObject(value)) {
          errMsg += this.parseModalStateValidationErrorObject(value);
        }
      }
    }
    return errMsg;
  }
}
