//----------------------------------------------------------------------------------------------------------------------
// <auto-generated />
// This code was generated by the Microsoft.Quantum.Qir.Runtime.Tools package.
// The purpose of this source code file is to provide an entry-point for executing a QIR program.
// It handles parsing of command line arguments, and it invokes an entry-point function exposed by the QIR program.
//----------------------------------------------------------------------------------------------------------------------

#include <fstream>
#include <iostream>
#include <map>
#include <memory>
#include <vector>

#include "CLI11.hpp"

using namespace std;

// Auxiliary functions for interop with Q# Pauli type.
map<string, char> PauliMap{
    {"PauliI", 0},
    {"PauliX", 1},
    {"PauliY", 3},
    {"PauliZ", 2}
};

extern "C" void UsePauliArg(
    char PauliArg
); // QIR interop function.

int main(int argc, char* argv[])
{
    CLI::App app("QIR Standalone Entry Point");

    // Add a command line option for each entry-point parameter.
    char PauliArgCli;
    PauliArgCli = 0;
    app.add_option("--PauliArg", PauliArgCli, "Option to provide a value for the PauliArg parameter")
        ->required()
        ->transform(CLI::CheckedTransformer(PauliMap, CLI::ignore_case));

    // After all the options have been added, parse arguments from the command line.
    CLI11_PARSE(app, argc, argv);

    // Cast parsed arguments to its interop types.
    char PauliArgInterop = PauliArgCli;

    // Execute the entry point operation.
    UsePauliArg(
        PauliArgInterop
    );

    // Flush the output of the simulation.
    cout.flush();

    return 0;
}
