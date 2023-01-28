import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { User } from '../../../../data/interfaces/user';
import { UserService } from '../../../core/services/api/user/user.service';
import { Clipboard } from '@angular/cdk/clipboard';

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

  constructor(private readonly userService: UserService, private readonly formBuilder: FormBuilder, private readonly clipboard: Clipboard) {
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

}
