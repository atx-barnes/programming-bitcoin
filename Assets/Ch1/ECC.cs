using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Numerics;

public class ECC : MonoBehaviour
{
    private void Start() {

        FieldElement a = new FieldElement(3, 13);

        FieldElement b = new FieldElement(12, 13);

        FieldElement c = new FieldElement(10, 13);

        print(a * c == b);
    }
}

/// <summary>
/// 
/// </summary>
public class FieldElement {

    /// <summary>
    /// 
    /// </summary>
    public int Num;

    /// <summary>
    /// 
    /// </summary>
    public int Prime;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="num"></param>
    /// <param name="prime"></param>
    public FieldElement(int num, int prime) {

        Intitialize(num, prime);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="num"></param>
    /// <param name="prime"></param>
    /// <returns></returns>
    public FieldElement Intitialize(int num, int prime) {

        if (num >= prime || num < 0) { throw new UnityException($"Num {num} not in field range 0 to {prime - 1}"); }

        Num = num;

        Prime = prime;

        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static bool operator ==(FieldElement self, FieldElement other) => self.Num == other.Num && self.Prime == other.Prime;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static bool operator !=(FieldElement self, FieldElement other) => self.Num != other.Num && self.Prime != other.Prime;

    /// <summary>
    /// Finite field addition operation.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static FieldElement operator +(FieldElement self, FieldElement other) {

        if(self.Prime != other.Prime) { throw new UnityException("Cannot add two numbers in different Fields"); }

        int num = Mod(self.Num + other.Num, self.Prime);

        return self.Intitialize(num, self.Prime);
    }

    /// <summary>
    /// Finite field substraction operation.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static FieldElement operator -(FieldElement self, FieldElement other) {

        if (self.Prime != other.Prime) { throw new UnityException("Cannot substract two numbers in different Fields"); }

        int num = Mod(self.Num - other.Num, self.Prime);

        return self.Intitialize(num, self.Prime);
    }

    /// <summary>
    /// Finite field multiplication operation.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static FieldElement operator *(FieldElement self, FieldElement other) {

        if (self.Prime != other.Prime) { throw new UnityException("Cannot multiple two numbers in different Fields"); }

        int num = Mod(self.Num * other.Num, self.Prime);

        return self.Intitialize(num, self.Prime);
    }

    /// <summary>
    /// Finite field exponentiation operation.
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
    /// Finite field division operation.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static FieldElement operator /(FieldElement self, FieldElement other) {

        if(self.Prime != other.Prime) { throw new UnityException("Cannot divide two numbers in different Fields"); }

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
