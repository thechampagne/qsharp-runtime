// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.Quantum.Intrinsic {
    open Microsoft.Quantum.Targeting;

    /// # Summary
    /// Applies the two qubit Ising $XX$ rotation gate.
    ///
    /// # Description
    /// \begin{align}
    ///     R_xx(\theta) \mathrel{:=}
    ///     \begin{bmatrix}
    ///         \cos \theta & 0 & 0 & -i\sin \theta  \\\\
    ///         0 & \cos \theta & -i\sin \theta & 0  \\\\
    ///         0 & -i\sin \theta & \cos \theta & 0  \\\\
    ///         -i\sin \theta & 0 & 0 & \cos \theta
    ///     \end{bmatrix}.
    /// \end{align}
    ///
    /// # Input
    /// ## theta
    /// The angle about which the qubits are rotated.
    /// ## qubit0
    /// The first qubit input to the gate.
    /// ## qubit1
    /// The second qubit input to the gate.
    @TargetInstruction("rxx__body")
    internal operation ApplyUncontrolledRxx (theta : Double, qubit0 : Qubit, qubit1 : Qubit) : Unit {
        body intrinsic;
    }
}