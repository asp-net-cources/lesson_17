﻿using Lesson17.Data.Models;

namespace Lesson17.Models;

public class AccessoriesModel : ProductModel
{
    public new ProductType ProductType { get; } = ProductType.Accessories;
}
