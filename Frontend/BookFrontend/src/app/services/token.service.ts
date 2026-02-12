import {HttpInterceptorFn, HttpRequest, HttpHandlerFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {LoginService} from './login.service';

export const authInterceptor: HttpInterceptorFn = (req:HttpRequest<any>, next:HttpHandlerFn) => {

  const auth = inject(LoginService);
  const token = auth.getToken();

  if(!token || req.url.includes('/login'))
  {
    return next(req);
  }

  const cloned = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`
    }
  });

  return next(cloned);
}
