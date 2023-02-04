import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-create-chat-dialog',
  templateUrl: './create-chat-dialog.component.html',
  styleUrls: ['./create-chat-dialog.component.css']
})
export class CreateChatDialogComponent implements OnInit {

  form = this.formBuilder.group({
    name: ['', Validators.required],
    role: ['', Validators.required]
  });

  constructor(public readonly formBuilder: FormBuilder, public readonly dialogRef: MatDialogRef<CreateChatDialogComponent>) { }

  ngOnInit(): void {
  }

  sendMessage() {
    this.dialogRef.close({ event: "done", data: this.form.value });
  }

  show() {
    console.log(this.form.value);
  }

}
