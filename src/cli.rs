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
        text: String,
    },
    /// Translate Morse code to text
    FromMorse {
        morse: String,
    },
}