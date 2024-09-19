#include "MarkdownRenderer.h"
#include <sstream>
#include <regex>

std::string MarkdownRenderer::render(const std::string& markdown) {
    std::vector<std::string> lines = splitLines(markdown);
    std::string result;
    bool inCodeBlock = false;

    for (const auto& line : lines) {
        if (line.substr(0, 3) == "```") {
            inCodeBlock = !inCodeBlock;
            result += Color::CYAN + line + Color::RESET + "\n";
            continue;
        }

        if (inCodeBlock) {
            result += Color::CYAN + line + Color::RESET + "\n";
        }
        else {
            result += parseLine(line);
        }
    }

    return result;
}

std::string MarkdownRenderer::parseLine(const std::string& line) {
    // Headings
    if (line.substr(0, 2) == "# ") return Color::GREEN + Color::BOLD + line.substr(2) + Color::RESET + "\n";
    if (line.substr(0, 3) == "## ") return Color::YELLOW + Color::BOLD + line.substr(3) + Color::RESET + "\n";
    if (line.substr(0, 4) == "### ") return Color::BLUE + Color::BOLD + line.substr(4) + Color::RESET + "\n";
    if (line.substr(0, 5) == "#### ") return Color::MAGENTA + Color::BOLD + line.substr(5) + Color::RESET + "\n";
    if (line.substr(0, 6) == "##### ") return Color::RED + Color::BOLD + line.substr(6) + Color::RESET + "\n";
    if (line.substr(0, 7) == "###### ") return Color::CYAN + Color::BOLD + line.substr(7) + Color::RESET + "\n";

    // Unordered list
    if (line.substr(0, 2) == "- " || line.substr(0, 2) == "* ") {
        return "  • " + parseInline(line.substr(2)) + "\n";
    }

    // Ordered list
    std::regex ol_regex("^(\\d+)\\. (.*)$");
    std::smatch matches;
    if (std::regex_search(line, matches, ol_regex)) {
        return "  " + matches[1].str() + ". " + parseInline(matches[2].str()) + "\n";
    }

    // Blockquote
    if (line.substr(0, 2) == "> ") {
        return Color::YELLOW + "│ " + parseInline(line.substr(2)) + Color::RESET + "\n";
    }

    // Horizontal rule
    if (line == "---" || line == "***" || line == "___") {
        return Color::YELLOW + std::string(40, '─') + Color::RESET + "\n";
    }

    return parseInline(line) + "\n";
}

std::string MarkdownRenderer::parseInline(const std::string& text) {
    std::string result = text;

    // Bold
    result = std::regex_replace(result, std::regex("\\*\\*(.*?)\\*\\*"), Color::BOLD + "$1" + Color::RESET);

    // Italic
    result = std::regex_replace(result, std::regex("\\*(.*?)\\*"), Color::ITALIC + "$1" + Color::RESET);

    // Inline code
    result = std::regex_replace(result, std::regex("`(.*?)`"), Color::CYAN + "$1" + Color::RESET);

    // Links
    result = std::regex_replace(result, std::regex("\\[(.*?)\\]\\((.*?)\\)"),
        Color::BLUE + Color::UNDERLINE + "$1" + Color::RESET + " (" + Color::CYAN + "$2" + Color::RESET + ")");

    // Images
    result = std::regex_replace(result, std::regex("!\\[(.*?)\\]\\((.*?)\\)"),
        "[Image: " + Color::MAGENTA + "$1" + Color::RESET + "] (" + Color::CYAN + "$2" + Color::RESET + ")");

    return result;
}

std::vector<std::string> MarkdownRenderer::splitLines(const std::string& text) {
    std::vector<std::string> lines;
    std::istringstream stream(text);
    std::string line;
    while (std::getline(stream, line)) {
        lines.push_back(line);
    }
    return lines;
}