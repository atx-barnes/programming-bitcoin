using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Numerics;

/// <summary>
/// Field element class.
/// </summary>
public class FieldElement {

    /// <summary>
    /// Integer field element.
    /// </summary>
    public int Num;

    /// <summary>
    /// Field size.
    /// </summary>
    public int Prime;

    /// <summary>
    /// Constructor for field element.
    /// </summary>
    /// <param name="num"></param>
    /// <param name="prime"></param>
    public FieldElement(int num, int prime) {

        Intitialize(num, prime);
    }

    /// <summary>
    /// Initialize field element once instantiated or also to pass back instance after operation.
    /// </summary>
    /// <param name="num"></param>
    /// <param name="prime"></param>
    /// <returns></returns>
    public FieldElement Intitialize(int num, int prime) {

        if (num >= prime || num < 0) { throw new Exception($"Num {num} not in field range 0 to {prime - 1}"); }

        Num = num;

        Prime = prime;

        return this;
    }

    /// <summary>
    /// Check to see if two elements are equal.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static bool operator ==(FieldElement self, FieldElement other) => self.Num == other.Num && self.Prime == other.Prime;

    /// <summary>
    /// Check to see if two elements are not equal.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static bool operator !=(FieldElement self, FieldElement other) => self.Num != other.Num && self.Prime != other.Prime;

    /// <summary>
    /// Overloaded finite field addition operation.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static FieldElement operator +(FieldElement self, FieldElement other) {

        if (self.Prime != other.Prime) { throw new Exception("Cannot add two numbers in different Fields"); }

        int num = Mod(self.Num + other.Num, self.Prime);

        return self.Intitialize(num, self.Prime);
    }

    /// <summary>
    /// Overloaded finite field substraction operation.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static FieldElement operator -(FieldElement self, FieldElement other) {

        if (self.Prime != other.Prime) { throw new Exception("Cannot substract two numbers in different Fields"); }

        int num = Mod(self.Num - other.Num, self.Prime);

        return self.Intitialize(num, self.Prime);
    }

    /// <summary>
    /// Overloaded finite field multiplication operation.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static FieldElement operator *(FieldElement self, FieldElement other) {

        if (self.Prime != other.Prime) { throw new Exception("Cannot multiple two numbers in different Fields"); }

        int num = Mod(self.Num * other.Num, self.Prime);

        return self.Intitialize(num, self.Prime);
    }

    /// <summary>
    /// Overloaded finite field exponentiation operation.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="exponent"></param>
    /// <returns></returns>
    public static FieldElement operator *(FieldElement self, int exponent) {

        int n = Mod(exponent, self.Prime - 1);

        int num = (int)BigInteger.ModPow(self.Num, n, self.Prime);

        return self.Intitialize(num, self.Prime);
    }

    /// <summary>
    /// Overloaded finite field division operation.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static FieldElement operator /(FieldElement self, FieldElement other) {

        if (self.Prime != other.Prime) { throw new Exception("Cannot divide two numbers in different Fields"); }

        int num = Mod(self.Num * (int)BigInteger.ModPow(other.Num, self.Prime - 2, self.Prime), self.Prime);

        return self.Intitialize(num, self.Prime);
    }

    /// <summary>
    /// Modulo operation.
    /// </summary>
    /// <param name="n"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    public static int Mod(int n, int m) {

        return ((n % m) + m) % m;
    }

    public override bool Equals(object obj) {

        return base.Equals(obj);
    }

    public override int GetHashCode() {

        return base.GetHashCode();
    }
}
