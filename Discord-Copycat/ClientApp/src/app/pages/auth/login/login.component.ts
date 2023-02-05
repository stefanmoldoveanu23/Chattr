import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms'
import { UserService } from '../../../core/services/api/user/user.service';
import { AuthService } from '../../../core/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  failed = false;
  hide = true;

  loginForm = this.formBuilder.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });

  constructor(private readonly formBuilder: FormBuilder, private readonly authService: AuthService, private readonly userService: UserService) { }

  ngOnInit(): void {
  }

  onLogin() {
    this.authService.login(this.loginForm.value).subscribe(
      () => {
        if (!this.authService.isLoggedIn()) {
          this.failed = true;
        } else {
          this.failed = false;
        }
      },
      error => console.error(error)
    )
  }

}
