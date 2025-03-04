import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (request, next) => {
  var isLocalStorageAvailable = typeof localStorage !== 'undefined';

  if (isLocalStorageAvailable) {
    const token = localStorage.getItem('token') ?? '';

    request = request.clone({
      setHeaders: {
        Authorization: token ? `Bearer ${token}` : '',
      },
    });
  }

  return next(request);
};
