﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Microsoft.Quantum.Simulation.Core;
using static System.Math;

namespace Microsoft.Quantum.Simulation.Simulators
{
    public partial class CommonNativeSimulator
    {
        // `QSimAssertProb` makes an impression that it is never used,
        // but since it inherits from Quantum.Diagnostics.AssertMeasurementProbability
        // (which is a C# class that corresponds to a Q# operation in our core libraries), it will be automatically used.
        // It is instantiated via reflection, hence we don't see it easily in the code.
        public class QSimAssertProb : Microsoft.Quantum.Diagnostics.AssertMeasurementProbability
        {
            private CommonNativeSimulator Simulator { get; }

            public QSimAssertProb(CommonNativeSimulator m) : base(m)
            {
                this.Simulator = m;
            }

            public override Func<(IQArray<Pauli>, IQArray<Qubit>, Result, double, string, double), QVoid> __Body__ => (_args) =>
            {
                var (paulis, qubits, result, expectedPr, msg, tol) = _args;

                Simulator.CheckAndPreserveQubits(qubits);

                if (paulis.Length != qubits.Length)
                {
                    IgnorableAssert.Assert((paulis.Length != qubits.Length), "Arrays length mismatch");
                    throw new InvalidOperationException($"Both input arrays for {this.GetType().Name} (paulis,qubits), must be of same size.");
                }

                // Capture original expectedPr for clarity in failure logging later.
                var originalExpectedPr = expectedPr;

                if (result == Result.Zero)
                {
                    expectedPr = 1 - expectedPr;
                }
                var ensemblePr = this.Simulator.JointEnsembleProbability((uint)paulis.Length, paulis.ToArray(), qubits.GetIds());

                if (Abs(ensemblePr - expectedPr) > tol)
                {
                    string extendedMsg;
                    if (result == Result.Zero)
                    {
                        // To account for the modification of expectedPr to (1 - expectedPr) when (result == Result.Zero), 
                        // we must also update the ensemblePr to (1 - ensemblePr) when reporting the failure.
                        extendedMsg = $"{msg}\n\tExpected:\t{originalExpectedPr}\n\tActual:\t{(1 - ensemblePr)}";
                    }
                    else
                    {
                        extendedMsg = $"{msg}\n\tExpected:\t{originalExpectedPr}\n\tActual:\t{ensemblePr}";
                    }
                    
                    IgnorableAssert.Assert(false, extendedMsg);
                    throw new ExecutionFailException(extendedMsg);
                }

                return QVoid.Instance;
            };

            public override Func<(IQArray<Pauli>, IQArray<Qubit>, Result, double, string, double), QVoid> __AdjointBody__ => (_args) => { return QVoid.Instance; };

            public override Func<(IQArray<Qubit>, (IQArray<Pauli>, IQArray<Qubit>, Result, double, string, double)), QVoid> __ControlledBody__ => (_args) => { return QVoid.Instance; };

            public override Func<(IQArray<Qubit>, (IQArray<Pauli>, IQArray<Qubit>, Result, double, string, double)), QVoid> __ControlledAdjointBody__ => (_args) => { return QVoid.Instance; };
        }
    }
}
