use std::io::{stdout, Write};
use crossterm::{
    execute,
    terminal::{Clear, ClearType},
    cursor::{MoveTo, Hide, Show},
    event::{read, Event, KeyCode},
    style::{Color, SetForegroundColor, ResetColor},
};
use crate::translator::MorseTranslator;

pub fn run_terminal_ui() -> crossterm::Result<()> {
    let mut text = String::new();
    let mut morse = String::new();
    let translator = MorseTranslator::new();
    let mut active_field = 0; // 0 for text, 1 for morse

    execute!(stdout(), Hide)?;

    loop {
        execute!(
            stdout(),
            Clear(ClearType::All),
            MoveTo(0, 0),
            SetForegroundColor(Color::Blue),
        )?;
        println!("Morse Code Translator (Press ESC to exit, TAB to switch fields)");
        println!();

        // Text field
        if active_field == 0 {
            execute!(stdout(), SetForegroundColor(Color::Blue))?;
            print!("Text:  ");
            execute!(stdout(), SetForegroundColor(Color::Green))?;
        } else {
            execute!(stdout(), SetForegroundColor(Color::White))?;
            print!("Text:  ");
        }
        println!("{}", if text.is_empty() { "<type here>" } else { &text });

        // Morse field
        if active_field == 1 {
            execute!(stdout(), SetForegroundColor(Color::Blue))?;
            print!("Morse: ");
            execute!(stdout(), SetForegroundColor(Color::Green))?;
        } else {
            execute!(stdout(), SetForegroundColor(Color::White))?;
            print!("Morse: ");
        }
        println!("{}", if morse.is_empty() { "<type here>" } else { &morse });

        execute!(stdout(), ResetColor)?;
        stdout().flush()?;

        match read()? {
            Event::Key(event) => match event.code {
                KeyCode::Esc => break,
                KeyCode::Tab => {
                    active_field = 1 - active_field;
                    if active_field == 0 {
                        text = translator.from_morse(&morse);
                    } else {
                        morse = translator.to_morse(&text);
                    }
                },
                KeyCode::Backspace => {
                    if active_field == 0 {
                        text.pop();
                        morse = translator.to_morse(&text);
                    } else {
                        morse.pop();
                        text = translator.from_morse(&morse);
                    }
                },
                KeyCode::Char(c) => {
                    if active_field == 0 {
                        text.push(c);
                        morse = translator.to_morse(&text);
                    } else {
                        morse.push(c);
                        text = translator.from_morse(&morse);
                    }
                },
                _ => {}
            },
            _ => {}
        }
    }

    execute!(stdout(), Show)?;
    Ok(())
}