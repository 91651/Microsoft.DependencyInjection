# Microsoft.DependencyInjection.Plus

## Summary

This package is mainly increased `Microsoft.Extensions.DependencyInjection`. Simplify the use of `DI`.


## Usage

#### 1. Inject all types from the assembly

```
services.AddScopedFromAssembly<Type>();
// or
services.AddSingletonFromAssembly<Type>();
// or
services.AddTransientFromAssembly<Type>();

```

#### 2. Inject all types from the assembly

```
services.AddScopedFromAssembly(typeof(Type));
// or
services.AddSingletonFromAssembly(typeof(Type));
// or
services.AddTransientFromAssembly(typeof(Type));
```

#### 3. Inject all types from the assembly

```
services.AddScopedFromAssembly("assembly");
// or
services.AddSingletonFromAssembly("assembly");
// or
services.AddTransientFromAssembly("assembly");
```

#### 3. Inject all types from the assembly

```
services.AddScopedFromAssembly(nameof(Assembly), o => o.Matching = true);
// or
services.AddSingletonFromAssembly(nameof(Assembly), o => o.Matching = true);
// or
services.AddTransientFromAssembly(nameof(Assembly), o => o.Matching = true);
```

## Package Name: BC.Microsoft.DependencyInjection.Plus`


