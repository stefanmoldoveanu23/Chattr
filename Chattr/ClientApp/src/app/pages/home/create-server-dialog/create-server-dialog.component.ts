import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-create-server-dialog',
  templateUrl: './create-server-dialog.component.html',
  styleUrls: ['./create-server-dialog.component.css']
})
export class CreateServerDialogComponent implements OnInit {

  form = this.formBuilder.group({
    name: ['', Validators.required],
    description: ['']
  });

  constructor(public readonly formBuilder: FormBuilder, public readonly dialogRef: MatDialogRef<CreateServerDialogComponent>) { }

  ngOnInit(): void {
  }

  sendMessage() {
    this.dialogRef.close({ event: "done", data: this.form.value });
  }

}
