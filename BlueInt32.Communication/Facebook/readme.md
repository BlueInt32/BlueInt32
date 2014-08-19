Utiliser le Resolver Facebook permet de déterminer dans quel état de la fan-gate on est

```cs
FacebookStateEnum state = FacebookState.Resolve();
switch (state)
{
	case FacebookStateEnum.OutsideOfFacebook:
	//redirect to facebook
	break;
	case FacebookStateEnum.PageLiked:
	// show index
	break;
	case FacebookStateEnum.PageNotLiked:
	// show fan-gate
	break;
	default:
	break;
}
```

On peut utiliser cette méthode dans un filtre d'action.
