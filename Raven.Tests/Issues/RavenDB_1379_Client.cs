﻿using System.Linq;

using Raven.Client;
using Raven.Json.Linq;

using Xunit;
using Xunit.Extensions;

namespace Raven.Tests.Issues
{
	public class RavenDB_1379_Client : RavenTest
	{
	    [Theory]
	    [InlineData("voron")]
	    [InlineData("esent")]
	    public void PagingWithoutFilters(string requestedStorage)
	    {
	        using (var documentStore = NewDocumentStore(requestedStorage: requestedStorage))
	        {
	            documentStore.DocumentDatabase.Put("FooBar1", null, new RavenJObject(), new RavenJObject(), null);
	            documentStore.DocumentDatabase.Put("BarFoo2", null, new RavenJObject(), new RavenJObject(), null);
	            documentStore.DocumentDatabase.Put("FooBar3", null, new RavenJObject(), new RavenJObject(), null);
	            documentStore.DocumentDatabase.Put("FooBar11", null, new RavenJObject(), new RavenJObject(), null);
	            documentStore.DocumentDatabase.Put("FooBar12", null, new RavenJObject(), new RavenJObject(), null);
	            documentStore.DocumentDatabase.Put("FooBar21", null, new RavenJObject(), new RavenJObject(), null);
	            documentStore.DocumentDatabase.Put("FooBar5", null, new RavenJObject(), new RavenJObject(), null);
	            documentStore.DocumentDatabase.Put("BarFoo7", null, new RavenJObject(), new RavenJObject(), null);
	            documentStore.DocumentDatabase.Put("FooBar111", null, new RavenJObject(), new RavenJObject(), null);
	            documentStore.DocumentDatabase.Put("BarFoo6", null, new RavenJObject(), new RavenJObject(), null);
	            documentStore.DocumentDatabase.Put("FooBar6", null, new RavenJObject(), new RavenJObject(), null);
	            documentStore.DocumentDatabase.Put("FooBar8", null, new RavenJObject(), new RavenJObject(), null);

		        var fetchedDocuments = documentStore
					.DatabaseCommands
					.StartsWith("FooBar", string.Empty, 0, 4, exclude: string.Empty)
                    .ToList();

                var foundDocKeys = fetchedDocuments.Select(doc => doc.Key)
                                                   .ToList();

                Assert.Equal(4, foundDocKeys.Count);
                Assert.Contains("FooBar1", foundDocKeys);
                Assert.Contains("FooBar11", foundDocKeys);
                Assert.Contains("FooBar111", foundDocKeys);
                Assert.Contains("FooBar12", foundDocKeys);

				fetchedDocuments = documentStore
					.DatabaseCommands
					.StartsWith("FooBar", string.Empty, 4, 4, exclude: string.Empty)
					.ToList();

				foundDocKeys = fetchedDocuments.Select(doc => doc.Key)
												   .ToList();

                Assert.Equal(4, foundDocKeys.Count);
                Assert.Contains("FooBar21", foundDocKeys);
                Assert.Contains("FooBar3", foundDocKeys);
                Assert.Contains("FooBar5", foundDocKeys);
                Assert.Contains("FooBar6", foundDocKeys);

				fetchedDocuments = documentStore
					.DatabaseCommands
					.StartsWith("FooBar", string.Empty, 8, 4, exclude: string.Empty)
					.ToList();

				foundDocKeys = fetchedDocuments.Select(doc => doc.Key)
												   .ToList();

                Assert.Equal(1, foundDocKeys.Count);
                Assert.Contains("FooBar8", foundDocKeys);
	        }
	    }

		[Theory]
		[InlineData("voron")]
		[InlineData("esent")]
		public void PagingWithoutFiltersWithPagingInformation(string requestedStorage)
		{
			using (var documentStore = NewDocumentStore(requestedStorage: requestedStorage))
			{
				documentStore.DocumentDatabase.Put("FooBar1", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("BarFoo2", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar3", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar11", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar12", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar21", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar5", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("BarFoo7", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar111", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("BarFoo6", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar6", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar8", null, new RavenJObject(), new RavenJObject(), null);

				var pagingInformation = new RavenPagingInformation();
				var fetchedDocuments = documentStore
					.DatabaseCommands
					.StartsWith("FooBar", string.Empty, 0, 4, pagingInformation: pagingInformation, exclude: string.Empty)
					.ToList();

				var foundDocKeys = fetchedDocuments.Select(doc => doc.Key)
												   .ToList();

				Assert.Equal(4, foundDocKeys.Count);
				Assert.Contains("FooBar1", foundDocKeys);
				Assert.Contains("FooBar11", foundDocKeys);
				Assert.Contains("FooBar111", foundDocKeys);
				Assert.Contains("FooBar12", foundDocKeys);

				fetchedDocuments = documentStore
					.DatabaseCommands
					.StartsWith("FooBar", string.Empty, 4, 4, pagingInformation: pagingInformation, exclude: string.Empty)
					.ToList();

				foundDocKeys = fetchedDocuments.Select(doc => doc.Key)
												   .ToList();

				Assert.Equal(4, foundDocKeys.Count);
				Assert.Contains("FooBar21", foundDocKeys);
				Assert.Contains("FooBar3", foundDocKeys);
				Assert.Contains("FooBar5", foundDocKeys);
				Assert.Contains("FooBar6", foundDocKeys);

				fetchedDocuments = documentStore
					.DatabaseCommands
					.StartsWith("FooBar", string.Empty, 8, 4, pagingInformation: pagingInformation, exclude: string.Empty)
					.ToList();

				foundDocKeys = fetchedDocuments.Select(doc => doc.Key)
												   .ToList();

				Assert.Equal(1, foundDocKeys.Count);
				Assert.Contains("FooBar8", foundDocKeys);
			}
		}

		[Theory]
		[InlineData("voron")]
		[InlineData("esent")]
		public void PagingWithoutFiltersAsync(string requestedStorage)
		{
			using (var documentStore = NewDocumentStore(requestedStorage: requestedStorage))
			{
				documentStore.DocumentDatabase.Put("FooBar1", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("BarFoo2", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar3", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar11", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar12", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar21", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar5", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("BarFoo7", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar111", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("BarFoo6", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar6", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar8", null, new RavenJObject(), new RavenJObject(), null);

				var fetchedDocuments = documentStore
					.AsyncDatabaseCommands
					.StartsWithAsync("FooBar", string.Empty, 0, 4, exclude: string.Empty)
					.Result
					.ToList();

				var foundDocKeys = fetchedDocuments.Select(doc => doc.Key)
												   .ToList();

				Assert.Equal(4, foundDocKeys.Count);
				Assert.Contains("FooBar1", foundDocKeys);
				Assert.Contains("FooBar11", foundDocKeys);
				Assert.Contains("FooBar111", foundDocKeys);
				Assert.Contains("FooBar12", foundDocKeys);

				fetchedDocuments = documentStore
					.AsyncDatabaseCommands
					.StartsWithAsync("FooBar", string.Empty, 4, 4, exclude: string.Empty)
					.Result
					.ToList();

				foundDocKeys = fetchedDocuments.Select(doc => doc.Key)
												   .ToList();

				Assert.Equal(4, foundDocKeys.Count);
				Assert.Contains("FooBar21", foundDocKeys);
				Assert.Contains("FooBar3", foundDocKeys);
				Assert.Contains("FooBar5", foundDocKeys);
				Assert.Contains("FooBar6", foundDocKeys);

				fetchedDocuments = documentStore
					.AsyncDatabaseCommands
					.StartsWithAsync("FooBar", string.Empty, 8, 4, exclude: string.Empty)
					.Result
					.ToList();

				foundDocKeys = fetchedDocuments.Select(doc => doc.Key)
												   .ToList();

				Assert.Equal(1, foundDocKeys.Count);
				Assert.Contains("FooBar8", foundDocKeys);
			}
		}

		[Theory]
		[InlineData("voron")]
		[InlineData("esent")]
		public void PagingWithoutFiltersWithPagingInformationAsync(string requestedStorage)
		{
			using (var documentStore = NewDocumentStore(requestedStorage: requestedStorage))
			{
				documentStore.DocumentDatabase.Put("FooBar1", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("BarFoo2", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar3", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar11", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar12", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar21", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar5", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("BarFoo7", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar111", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("BarFoo6", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar6", null, new RavenJObject(), new RavenJObject(), null);
				documentStore.DocumentDatabase.Put("FooBar8", null, new RavenJObject(), new RavenJObject(), null);

				var pagingInformation = new RavenPagingInformation();
				var fetchedDocuments = documentStore
					.AsyncDatabaseCommands
					.StartsWithAsync("FooBar", string.Empty, 0, 4, pagingInformation: pagingInformation, exclude: string.Empty)
					.Result
					.ToList();

				var foundDocKeys = fetchedDocuments.Select(doc => doc.Key)
												   .ToList();

				Assert.Equal(4, foundDocKeys.Count);
				Assert.Contains("FooBar1", foundDocKeys);
				Assert.Contains("FooBar11", foundDocKeys);
				Assert.Contains("FooBar111", foundDocKeys);
				Assert.Contains("FooBar12", foundDocKeys);

				fetchedDocuments = documentStore
					.AsyncDatabaseCommands
					.StartsWithAsync("FooBar", string.Empty, 4, 4, pagingInformation: pagingInformation, exclude: string.Empty)
					.Result
					.ToList();

				foundDocKeys = fetchedDocuments.Select(doc => doc.Key)
												   .ToList();

				Assert.Equal(4, foundDocKeys.Count);
				Assert.Contains("FooBar21", foundDocKeys);
				Assert.Contains("FooBar3", foundDocKeys);
				Assert.Contains("FooBar5", foundDocKeys);
				Assert.Contains("FooBar6", foundDocKeys);

				fetchedDocuments = documentStore
					.AsyncDatabaseCommands
					.StartsWithAsync("FooBar", string.Empty, 8, 4, pagingInformation: pagingInformation, exclude: string.Empty)
					.Result
					.ToList();

				foundDocKeys = fetchedDocuments.Select(doc => doc.Key)
												   .ToList();

				Assert.Equal(1, foundDocKeys.Count);
				Assert.Contains("FooBar8", foundDocKeys);
			}
		}
	}
}
