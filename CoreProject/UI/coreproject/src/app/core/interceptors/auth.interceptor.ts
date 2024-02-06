import { HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  if (shouldInterceptRequest(req)) {
    const cookieService = inject(CookieService);
    const token = cookieService.get('Authorization');
    if (token) {
      const cloned = req.clone({
        setHeaders: {
          Authorization: token,
        },
      });
      return next(cloned);
    } else {
      return next(req);
    }
  }
  return next(req);
};

function shouldInterceptRequest(request: HttpRequest<any>): boolean {
  return request.urlWithParams.indexOf('addAuth=true', 0) > -1 ? true : false;
}
