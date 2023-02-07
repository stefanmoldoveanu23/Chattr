import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { User } from '../../../../data/interfaces/user';
import { UserService } from '../../../core/services/api/user/user.service';
import { Clipboard } from '@angular/cdk/clipboard';
import { AuthService } from '../../../core/services/auth/auth.service';
import { Themes } from '../../../../data/enums/themes';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  self: User = { id: '', username: '', email: '', token: '' };

  addFriend = this.formBuilder.group({
    id: ['', Validators.required]
  });

  friends: User[] = [{ id: 'hi', username: 'MY', email: '', token: '' }];

  updateData = this.formBuilder.group({
    username: [''],
    password: [''],
    email: ['', Validators.email],
    theme: [''],
    notifs: [''],
    appearance: [''],
  });

  passwordHide: boolean = true;

  Themes = Themes;

  constructor(private readonly authService: AuthService, private readonly userService: UserService, private readonly formBuilder: FormBuilder, private readonly clipboard: Clipboard) {
    this.userService.getFriends().subscribe(
      friends => {
        if (friends.length > 0) {
          this.friends = friends;
        }
      },
      error => console.error(error)
    );

    this.userService.getSelf().subscribe(
      user => this.self = user,
      error => console.error("Error getting self " + error)
    );
  }

  ngOnInit(): void {
  }

  onAddFriend() {
    if (this.addFriend.value.id != null) {
      this.userService.addFriend(this.addFriend.value.id).subscribe(
        () => window.location.reload(),
        error => console.error("Error adding friend " + error)
      );
    }
  }

  onRemoveFriend(friendId: string) {
    this.userService.removeFriend(friendId).subscribe(
      () => window.location.reload(),
      error => console.error("Error removing friend " + error)
    );
  }

  onCopyId() {
    this.clipboard.copy(this.self.id);
  }

  onSaveChanges() {
    console.log(this.updateData.value);
  }

  onLogout() {
    this.authService.logout();
  }

  onDelete() {
    this.userService.deleteAccount().subscribe(
      () => this.authService.logout(),
      error => {
        console.log('Error deleting account.');
        console.error(error);
      }
    );
  }

}
