using NBitcoin.Secp256k1;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WalletWasabi.Tests.UnitTests.Crypto
{
	public class GroupElementComparisionTests
	{
		[Fact]
		public void OneOneEqualsOneOne()
		{
			var one = new FE(1);
			var a = new GE(one, one);
			var b = new GE(one, one);
			Assert.True(Secp256k1Helpers.Equals(a, b));
		}

		[Fact]
		public void OneTwoDoesntEqualOneOne()
		{
			var one = new FE(1);
			var two = new FE(2);
			var a = new GE(one, two);
			var b = new GE(one, one);
			Assert.False(Secp256k1Helpers.Equals(a, b));
		}

		[Fact]
		public void TwoOneDoesntEqualOneOne()
		{
			var one = new FE(1);
			var two = new FE(2);
			var a = new GE(one, one);
			var b = new GE(two, one);
			Assert.False(Secp256k1Helpers.Equals(a, b));
		}

		[Fact]
		public void InfinityDoesntEqualNotInfinity()
		{
			var one = new FE(1);
			var a = new GE(one, one);
			var b = new GE(one, one, infinity: true);
			Assert.False(Secp256k1Helpers.Equals(a, b));
		}

		[Fact]
		public void InfinityDoesntCareCoordinates()
		{
			var one = new FE(1);
			var two = new FE(2);
			var a = new GE(one, one, infinity: true);
			var b = new GE(two, one, infinity: true);
			Assert.True(Secp256k1Helpers.Equals(a, b));
		}

		[Fact]
		public void MagnitudeDoesntMatter()
		{
			// ToDo: is this ok?
			var one = new FE(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, magnitude: 0, normalized: false);
			var one2 = new FE(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, magnitude: 1, normalized: false);
			var a = new GE(one, one);
			var b = new GE(one2, one);
			Assert.True(Secp256k1Helpers.Equals(a, b));

			one = new FE(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, magnitude: 0, normalized: false);
			one2 = new FE(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, magnitude: 2, normalized: false);
			a = new GE(one, one);
			b = new GE(one2, one);
			Assert.True(Secp256k1Helpers.Equals(a, b));

			one = new FE(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, magnitude: 1, normalized: false);
			one2 = new FE(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, magnitude: 2, normalized: false);
			a = new GE(one, one);
			b = new GE(one2, one);
			Assert.True(Secp256k1Helpers.Equals(a, b));
		}

		[Fact]
		public void NormalizationDoesntMatter()
		{
			// ToDo: is this ok?
			var one = new FE(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, magnitude: 0, normalized: false);
			var one2 = new FE(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, magnitude: 0, normalized: true);
			var a = new GE(one, one);
			var b = new GE(one2, one);
			Assert.True(Secp256k1Helpers.Equals(a, b));
		}

		[Fact]
		public void OneOneOneEqualsOneOneOneJacobian()
		{
			var one = new FE(1);
			var a = new GEJ(one, one, one);
			var b = new GEJ(one, one, one);
			Assert.True(Secp256k1Helpers.Equals(a, b));
		}

		[Theory]
		[InlineData(2, 1, 1)]
		[InlineData(1, 2, 1)]
		[InlineData(1, 1, 2)]
		[InlineData(1, 2, 3)]
		[InlineData(2, 2, 1)]
		[InlineData(1, 2, 2)]
		[InlineData(2, 1, 2)]
		[InlineData(2, 2, 2)]
		public void DoesntEqualCombinationsJacobian(uint first, uint second, uint third)
		{
			var one = new FE(1);
			var firstFe = new FE(first);
			var secondFe = new FE(second);
			var thirdFe = new FE(third);

			var a = new GEJ(one, one, one);
			var b = new GEJ(firstFe, secondFe, thirdFe);
			Assert.False(Secp256k1Helpers.Equals(a, b));
		}

		[Fact]
		public void InfinityDoesntEqualNotInfinityJacobian()
		{
			var one = new FE(1);
			var a = new GEJ(one, one, one);
			var b = new GEJ(one, one, one, infinity: true);
			Assert.False(Secp256k1Helpers.Equals(a, b));
		}

		[Fact]
		public void InfinityDoesntCareCoordinatesJacobian()
		{
			var one = new FE(1);
			var two = new FE(2);
			var a = new GEJ(one, one, one, infinity: true);
			var b = new GEJ(two, one, one, infinity: true);
			Assert.True(Secp256k1Helpers.Equals(a, b));
		}

		[Fact]
		public void MagnitudeDoesntMatterJacobian()
		{
			// ToDo: is this ok?
			var one = new FE(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, magnitude: 0, normalized: false);
			var one2 = new FE(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, magnitude: 1, normalized: false);
			var a = new GEJ(one, one, one);
			var b = new GEJ(one2, one, one);
			Assert.True(Secp256k1Helpers.Equals(a, b));

			one = new FE(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, magnitude: 0, normalized: false);
			one2 = new FE(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, magnitude: 2, normalized: false);
			a = new GEJ(one, one, one);
			b = new GEJ(one2, one, one);
			Assert.True(Secp256k1Helpers.Equals(a, b));

			one = new FE(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, magnitude: 1, normalized: false);
			one2 = new FE(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, magnitude: 2, normalized: false);
			a = new GEJ(one, one, one);
			b = new GEJ(one2, one, one);
			Assert.True(Secp256k1Helpers.Equals(a, b));
		}

		[Fact]
		public void NormalizationDoesntMatterJacobian()
		{
			// ToDo: is this ok?
			var one = new FE(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, magnitude: 0, normalized: false);
			var one2 = new FE(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, magnitude: 0, normalized: true);
			var a = new GEJ(one, one, one);
			var b = new GEJ(one2, one, one);
			Assert.True(Secp256k1Helpers.Equals(a, b));
		}
	}
}
