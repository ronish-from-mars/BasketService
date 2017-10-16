namespace BasketService.ClientSdk
{
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for BasketApi.
    /// </summary>
    public static partial class BasketApiExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='customerId'>
            /// </param>
            public static void ApiBasketByCustomerIdGet(this IBasketApi operations, int customerId)
            {
                operations.ApiBasketByCustomerIdGetAsync(customerId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='customerId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task ApiBasketByCustomerIdGetAsync(this IBasketApi operations, int customerId, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.ApiBasketByCustomerIdGetWithHttpMessagesAsync(customerId, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='productDto'>
            /// </param>
            public static void ApiBasketPost(this IBasketApi operations, ProductDto productDto = default(ProductDto))
            {
                operations.ApiBasketPostAsync(productDto).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='productDto'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task ApiBasketPostAsync(this IBasketApi operations, ProductDto productDto = default(ProductDto), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.ApiBasketPostWithHttpMessagesAsync(productDto, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='productDto'>
            /// </param>
            public static void ApiBasketRemoveDelete(this IBasketApi operations, ProductDto productDto = default(ProductDto))
            {
                operations.ApiBasketRemoveDeleteAsync(productDto).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='productDto'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task ApiBasketRemoveDeleteAsync(this IBasketApi operations, ProductDto productDto = default(ProductDto), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.ApiBasketRemoveDeleteWithHttpMessagesAsync(productDto, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='productDto'>
            /// </param>
            public static void ApiBasketUpdatePut(this IBasketApi operations, ProductDto productDto = default(ProductDto))
            {
                operations.ApiBasketUpdatePutAsync(productDto).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='productDto'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task ApiBasketUpdatePutAsync(this IBasketApi operations, ProductDto productDto = default(ProductDto), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.ApiBasketUpdatePutWithHttpMessagesAsync(productDto, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='customerId'>
            /// </param>
            public static void ApiBasketClearDelete(this IBasketApi operations, int? customerId = default(int?))
            {
                operations.ApiBasketClearDeleteAsync(customerId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='customerId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task ApiBasketClearDeleteAsync(this IBasketApi operations, int? customerId = default(int?), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.ApiBasketClearDeleteWithHttpMessagesAsync(customerId, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static void ApiProductGet(this IBasketApi operations)
            {
                operations.ApiProductGetAsync().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task ApiProductGetAsync(this IBasketApi operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.ApiProductGetWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            public static void ApiProductByIdGet(this IBasketApi operations, int id)
            {
                operations.ApiProductByIdGetAsync(id).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task ApiProductByIdGetAsync(this IBasketApi operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.ApiProductByIdGetWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

    }
}
