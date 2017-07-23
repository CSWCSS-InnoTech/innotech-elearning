﻿namespace MathNet.Symbolics
{
    using System;
    using System.Numerics;
    using MathNet.Numerics;
    using C = System.Numerics.Complex;

    // this could be extended to arbitrary/custom precision approximations in the future
    class Approximation : TaggedUnion<double, Complex>
    {
        public Approximation(double x) : base(x) { }
        public Approximation(Complex x) : base(x) { }
        // Simpler usage in C#
        public static implicit operator Approximation(double x) => new Approximation(x);
        public static implicit operator Approximation(Complex x) => new Approximation(x);

        public double RealValue => Index == 1 ? Val1 : Index == 2 && Val2.IsReal() ? Val2.Real :
            throw new InvalidOperationException("Value not convertible to a real number.");

        public Complex ComplexValue => Index == 1 ? new Complex(Val1, 0) : Val2;

        public static Approximation FromRational(BigRational x) => new Approximation((double)x);

    let negate = function
        | Real a -> Real(-a)
        | Complex a -> Complex(-a)
    let sum = function
        | Real a, Real b -> Real(a+b)
        | Complex a, Complex b -> Complex(a+b)
        | Complex a, Real b | Real b, Complex a -> Complex(a + complex b 0.0)
    let product = function
        | Real a, Real b -> Real(a* b)
        | Complex a, Complex b -> Complex(a* b)
        | Complex a, Real b | Real b, Complex a -> Complex(a* complex b 0.0)
    let pow = function
        | Real a, Real b -> Real(a** b)
        | Complex a, Complex b -> Complex(C.Pow(a, b))
        | Real a, Complex b -> Complex(C.Pow(complex a 0.0, b))
        | Complex a, Real b -> Complex(C.Pow(a, complex b 0.0))
    let invert = function
        | Real a -> Real(1.0/a)
        | Complex a -> Complex(C.Reciprocal a)

    let abs = function
        | Real a -> Real(Math.Abs a)
        | Complex a -> Real(C.Abs a)
    let ln = function
        | Real a -> Real(Math.Log a)
        | Complex a -> Complex(C.Log a)
    let exp = function
        | Real a -> Real(Math.Exp a)
        | Complex a -> Complex(C.Exp a)
    let sin = function
        | Real a -> Real(Math.Sin a)
        | Complex a -> Complex(C.Sin a)
    let cos = function
        | Real a -> Real(Math.Cos a)
        | Complex a -> Complex(C.Cos a)
    let tan = function
        | Real a -> Real(Math.Tan a)
        | Complex a -> Complex(C.Tan a)
    let sinh = function
        | Real a -> Real(Math.Sinh a)
        | Complex a -> Complex(C.Sinh a)
    let cosh = function
        | Real a -> Real(Math.Cosh a)
        | Complex a -> Complex(C.Cosh a)
    let tanh = function
        | Real a -> Real(Math.Tanh a)
        | Complex a -> Complex(C.Tanh a)
    let asin = function
        | Real a -> Real(Math.Asin a)
        | Complex a -> Complex(C.Asin a)
    let acos = function
        | Real a -> Real(Math.Acos a)
        | Complex a -> Complex(C.Acos a)
    let atan = function
        | Real a -> Real(Math.Atan a)
        | Complex a -> Complex(C.Atan a)

    let cot = function
        | Real a -> Real(Trig.Cot a)
        | Complex a -> Complex(Complex.cot a)
    let sec = function
        | Real a -> Real(Trig.Sec a)
        | Complex a -> Complex(Complex.sec a)
    let csc = function
        | Real a -> Real(Trig.Csc a)
        | Complex a -> Complex(Complex.csc a)

    let apply f a =
        match f with
        | Abs -> abs a
        | Ln -> ln a
        | Exp -> exp a
        | Sin ->sin a
        | Cos -> cos a
        | Tan -> tan a
        | Cosh-> cosh a
        | Sinh -> sinh a
        | Tanh -> tanh a
        | Asin -> asin a
        | Acos -> acos a
        | Atan -> atan a
        | Cot -> cot a
        | Sec -> sec a
        | Csc -> csc a

    let isZero = function
        | Real x when x = 0.0 -> true
        | Complex c when c.IsZero() -> true
        | _ -> false
    let isOne = function
        | Real x when x = 1.0 -> true
        | Complex c when c = C.One-> true
        | _ -> false
    let isMinusOne = function
        | Real x when x = -1.0 -> true
        | Complex c when c.IsReal() && c.Real = -1.0 -> true
        | _ -> false
    let isPositive = function
        | Real x when x > 0.0 -> true
        | Complex c when c.IsReal() && c.Real > 0.0 -> true
        | _ -> false
    let isNegative = function
        | Real x when x< 0.0 -> true
        | Complex c when c.IsReal() && c.Real< 0.0 -> true
        | _ -> false

    let internal orderRelation(x:Approximation) (y:Approximation) =
        match x, y with
        | (Real x), (Real y) -> x<y
        | (Complex x), (Complex y) -> x.Real<y.Real || x.Real = y.Real && x.Imaginary<y.Imaginary
        | (Real x), (Complex y) -> not(y.IsReal()) || x<y.Real
        | (Complex x), (Real y) -> x.IsReal() && x.Real<y

    /// Sort approximations in a list with standard expression ordering.
    [< CompiledName("SortList") >]
    let sortList list =
        List.sortWith (fun a b -> if a = b then 0 elif orderRelation a b then -1 else 1) list
}