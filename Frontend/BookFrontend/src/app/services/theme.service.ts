import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class ThemeService {
  private darkMode = "darkMode"

  constructor(){
    const isDark = localStorage.getItem(this.darkMode) === 'true';
    this.setDarkMode(isDark);
  }

  setDarkMode(isDark: boolean){
    if(isDark){
      document.body.classList.add('dark');
    }else{
      document.body.classList.remove('dark');
    }
    localStorage.setItem(this.darkMode, isDark.toString());
  }

  toggleDarkMode(){
    const isDark = document.body.classList.contains('dark');
    this.setDarkMode(!isDark);
  }

  isDarkMode(){
    return  document.body.classList.contains('dark');
  }
}
