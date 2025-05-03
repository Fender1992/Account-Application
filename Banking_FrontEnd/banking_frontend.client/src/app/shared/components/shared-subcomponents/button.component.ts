import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  standalone: true,
  imports: [CommonModule]
})
export class ButtonComponent {
  @Input() label: string = '';
  @Input() type: 'primary' | 'secondary' | 'danger' | 'success' = 'primary';
  @Input() size: 'small' | 'medium' | 'large' = 'medium';
  @Input() disabled: boolean = false;
  @Input() icon: string | null = null;
  @Input() loading: boolean = false;

  @Output() onClick: EventEmitter<void> = new EventEmitter<void>();

  handleClick(): void {
    if (!this.disabled && !this.loading) {
      this.onClick.emit();
    }
  }
}
