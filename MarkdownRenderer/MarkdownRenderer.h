#pragma once
#include <string>
#include <vector>

class MarkdownRenderer {
public:
    static std::string render(const std::string& markdown);

private:
    static std::string parseLine(const std::string& line);
    static std::string parseInline(const std::string& text);
    static std::vector<std::string> splitLines(const std::string& text);
};

// ANSI codes
namespace Color {
    const std::string RESET = "\033[0m";
    const std::string BOLD = "\033[1m";
    const std::string ITALIC = "\033[3m";
    const std::string UNDERLINE = "\033[4m";
    const std::string BLACK = "\033[30m";
    const std::string RED = "\033[31m";
    const std::string GREEN = "\033[32m";
    const std::string YELLOW = "\033[33m";
    const std::string BLUE = "\033[34m";
    const std::string MAGENTA = "\033[35m";
    const std::string CYAN = "\033[36m";
    const std::string WHITE = "\033[37m";
}