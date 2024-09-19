mod translator;
mod terminal_ui;

use crate::terminal_ui::run_terminal_ui;

fn main() {
    if let Err(e) = run_terminal_ui() {
        eprintln!("Error in terminal UI: {}", e);
    }
}