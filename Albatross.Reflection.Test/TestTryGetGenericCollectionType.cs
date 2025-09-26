using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using Xunit;
#pragma warning disable CS0618 // Type or member is obsolete

namespace Albatross.Reflection.Test {
	public class TestTryGetGenericCollectionType {
		static IEnumerable<int> GetEnumerable() {
			yield return 1;
		}
		
		[Fact]
		public void TestRunTimeType() {
			Type type = GetEnumerable().GetType();
			Assert.True(type.TryGetGenericCollectionElementType(out Type? args));
			Assert.Same(typeof(int), args);
		}

		[Fact]
		public void TestIEnumerable_T() {
			Type type = typeof(IEnumerable<string>);
			Assert.True(type.TryGetGenericCollectionElementType(out Type? args));
			Assert.Same(typeof(string), args);
		}
		
		[Fact]
		public void TestIEnumerable() {
			Type type = typeof(IEnumerable);
			Assert.False(type.TryGetGenericCollectionElementType(out var _));
		}
		
		[Fact]
		public void TestIEnumerable_with_old() {
			Type type = typeof(IEnumerable);
			Assert.True(type.TryGetCollectionElementType(out var elementType));
			Assert.Same(typeof(object), elementType);
		}
		
		[Fact]
		public void TestIList() {
			Type type = typeof(IList);
			Assert.False(type.TryGetGenericCollectionElementType(out var _));
		}
		
		[Fact]
		public void TestIList_with_old() {
			Type type = typeof(IList);
			Assert.True(type.TryGetCollectionElementType(out var elementType));
			Assert.Same(typeof(object), elementType);
		}
		
		[Fact]
		public void TestArray() {
			Type type = typeof(string[]);
			Assert.True(type.TryGetGenericCollectionElementType(out Type? args));
			Assert.Same(typeof(string), args);
		}
		
		[Fact]
		public void TestArray2() {
			var type = Array.Empty<string>().GetType();
			Assert.True(type.TryGetGenericCollectionElementType(out Type? args));
			Assert.Same(typeof(string), args);
		}
		
		[Fact]
		public void TestGenericList() {
			Type type = typeof(List<string>);
			Assert.True(type.TryGetGenericCollectionElementType(out Type? args));
			Assert.Same(typeof(string), args);
		}

		[Fact]
		public void TestString() {
			Type type = typeof(string);
			Assert.False(type.TryGetGenericCollectionElementType(out Type? args));
			Assert.Null(args);
		}
	}
}
#pragma warning restore CS0618 // Type or member is obsolete
