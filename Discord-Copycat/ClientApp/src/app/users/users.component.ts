import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder} from '@angular/forms';
import { User } from '../../data/interfaces/User';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  private user: User = {
    username: '',
    password: '',
    email: '',
  };

  form1;
  baseUrl: string = 'https://localhost:7291/api/';

  constructor(private formBuilder: FormBuilder, private readonly http: HttpClient) {
    this.form1 = formBuilder.group(this.user);
  }

  ngOnInit(): void {
  }

  onSubmitCreate() {
    var username = this.form1.get('username');
    var password = this.form1.get('password');
    var email = this.form1.get('email');
    this.user = { username: username && username.value ? username.value : '', password: password && password.value ? password.value : '', email: email && email.value ? email.value : '' };

    this.http.post<User>(this.baseUrl + 'user/create-user', this.user).subscribe(result => console.log(result), error => console.error(error));
  }

  onSubmitFriend() {
    var username = this.form1.get('username');
    var password = this.form1.get('password');
    var email = this.form1.get('email');
    this.user = { username: username && username.value ? username.value : '', password: password && password.value ? password.value : '', email: email && email.value ? email.value : '' };

    this.http.post<User>(this.baseUrl + "user/add-friend/5e62067d-9979-4d44-5cfb-08daedad3613", this.user)
      .subscribe(result => console.log(result),
        error => console.error(error));

  }

}
