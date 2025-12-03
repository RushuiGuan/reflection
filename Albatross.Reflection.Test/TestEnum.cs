using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Albatross.Reflection.Test {
	public enum MyEnum {
		a, b, c, d, e, f,
	}
	public class TestEnum {
		[Fact]
		public void TestValueText() {
			object value = MyEnum.a;
			Assert.Equal("a", value.ToString());
			Assert.Equal("a", Enum.GetName(typeof(MyEnum), value));
		}
		
		public bool TestConflict([NotNullWhen(true)] out string? message) {
			message = null;
			return false;
		}
	}
}