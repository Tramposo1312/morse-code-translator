#include <SFML/Graphics.hpp>
#include <iostream>
#include <vector>
#include <string>
#include <algorithm>
#include <random>
#include <chrono>

class Player {
public:
    std::string name;
    bool isMafia;
    bool isAlive;

    Player(std::string n, bool m) : name(n), isMafia(m), isAlive(true) {}
};

class Game {
private:
    std::vector<Player> players;
    int mafiaCount;
    int civilianCount;

public:
    Game(int totalPlayers, int mafiaPlayers) {
        mafiaCount = mafiaPlayers;
        civilianCount = totalPlayers - mafiaPlayers;

        for (int i = 0; i < totalPlayers; i++) {
            std::string name = "Player" + std::to_string(i + 1);
            players.push_back(Player(name, i < mafiaPlayers));
        }

        unsigned seed = std::chrono::system_clock::now().time_since_epoch().count();
        shuffle(players.begin(), players.end(), std::default_random_engine(seed));
    }

    void playGame() {
        int day = 1;
        while (mafiaCount > 0 && civilianCount > mafiaCount) {
            std::cout << "\n--- Day " << day << " ---\n";
            nightPhase();
            if (mafiaCount == 0 || civilianCount <= mafiaCount) break;
            dayPhase();
            day++;
        }

        if (mafiaCount == 0) {
            std::cout << "Civilians win!\n";
        }
        else {
            std::cout << "Mafia wins!\n";
        }
    }

private:
    void nightPhase() {
        std::cout << "Night falls. Mafia chooses a victim.\n";
        int victim = rand() % players.size();
        while (!players[victim].isAlive || players[victim].isMafia) {
            victim = rand() % players.size();
        }
        players[victim].isAlive = false;
        if (!players[victim].isMafia) civilianCount--;
        std::cout << players[victim].name << " was killed during the night.\n";
    }

    void dayPhase() {
        std::cout << "Day breaks. Remaining players:\n";
        for (const auto& player : players) {
            if (player.isAlive) {
                std::cout << player.name << "\n";
            }
        }

        std::cout << "Players vote to eliminate someone:\n";
        int eliminated = rand() % players.size();
        while (!players[eliminated].isAlive) {
            eliminated = rand() % players.size();
        }
        players[eliminated].isAlive = false;
        if (players[eliminated].isMafia) mafiaCount--;
        else civilianCount--;
        std::cout << players[eliminated].name << " was eliminated by vote.\n";
    }
};

int main() {
    int totalPlayers, mafiaPlayers;
    std::cout << "Enter total number of players: ";
    std::cin >> totalPlayers;
    std::cout << "Enter number of mafia players: ";
    std::cin >> mafiaPlayers;

    if (mafiaPlayers >= totalPlayers || mafiaPlayers <= 0) {
        std::cout << "Invalid number of mafia players.\n";
        return 1;
    }

    Game game(totalPlayers, mafiaPlayers);
    game.playGame();

    return 0;
}