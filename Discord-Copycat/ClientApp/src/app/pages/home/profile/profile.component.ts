import { Component, OnInit } from '@angular/core';
import { User } from '../../../../data/interfaces/user';
import { UserService } from '../../../core/services/api/user/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  friends: User[] = [{ id: 'hi', username: 'my', email: '', token: '' }];

  constructor(private readonly userService: UserService) {
    userService.getFriends().subscribe(
      friends => this.friends = friends,
      error => console.error(error)
    );
  }

  ngOnInit(): void {
  }

}
