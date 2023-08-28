namespace Product.Application.Dtos.Input
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProductDto
    {
        /// <summary>
        ///
        /// </summary>
        /// <example>Refrigerator</example>
        public string? Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <example>HOME_APPLIANCES</example>
        public string? ProductCategory { get; set; }
    }
}