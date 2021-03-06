using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Numerics;

public class Point : MonoBehaviour
{
    public int A;

    public int B;

    public object X;

    public object Y;

    public Point(object x, object y, int a, int b) {

        A = a;

        B = b;

        X = (int)x;

        Y = (int)y;

        if(x == null && y == null) {

            return;
        }

        if ((int)Math.Pow((int)y,2) != (int)Math.Pow((int)x, 3) + a * (int)x + b) {

            throw new Exception($"({x}, {y}) is not on the curve");
        }
    }

    public static bool operator ==(Point self, Point other) => self.X == other.X && self.Y == other.Y && self.A == other.A && self.B == other.B;

    public static bool operator !=(Point self, Point other) => self.X != other.X && self.Y != other.Y && self.A != other.A && self.B != other.B;

    public static Point operator +(Point self, Point other) {

        if(self.A != other.A || self.B != other.B) {

            throw new Exception($"Points {self}, {other} are not on the same curve");
        }

        if(self.X == null) {

            return other;

        } else if(other.X == null) {

            return self;

        } else {

            throw new Exception($"Points {self}, {other} are not on the same curve");
        }
    }

    public override bool Equals(object other) {

        return base.Equals(other);
    }

    public override int GetHashCode() {

        return base.GetHashCode();
    }
}
