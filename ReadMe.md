# WebApi demo project

The architecture of App and degree of layers' separation depend on goals. This parameter is located on the scale 
from full layers isolation (but it's assume the high laboriousness) to implementation with trade-offs 
(not such complete separation and not such high laboriousness as well).
E.g. whether we should separate domain entities and persistence level's entities, produce generic repositories or 
specified ones, put out IQueryable outside of a repository. All of these depends, and there is no the only right 
decision in my opinion.

In this demo project I assumed that our goal is maximal separation. As if it's a outlining for a "big" App
which is developing by several teams etc. The Onion architecture was chosen for the same purpose.

There is an iffy moment in this demo project about dependency injection, about composition root to be more
precisely. I know that composition should be done in a single place and only that assembly should use IoC.
But I don't know how to do that with asp.core build-in IoC in a such way that composition process was 
implemented outside the presentation level.

## Now I'm going to:

- [x] Implement all functionality of App services (ILampsComparisonService) and put them in WebApi.
- [x] Add input data validation by Fluent Validation.
- [ ] Add front end to consume this WebApi.
- [ ] Add EF migrations.
- [ ] Add authentication by means of tokens.
