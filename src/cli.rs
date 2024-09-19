use clap::{Parser, Subcommand};

#[derive(Parser)]
#[clap(author, version, about, long_about = None)]
pub struct Cli {
    #[clap(subcommand)]
    pub command: Option<Commands>,
}

#[derive(Subcommand)]
pub enum Commands {
    /// Translate text to Morse code
    ToMorse {
        /// The text to translate
        text: String,
    },
    /// Translate Morse code to text
    FromMorse {
        /// The Morse code to translate
        morse: String,
    },
    /// Run the interactive terminal UI
    Interactive,
}