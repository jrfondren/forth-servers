# forth-servers
A series of increasingly complex servers in Forth

Adapted from the examples in the SwiftForth-only [Writing servers in
Forth](https://groups.google.com/d/msg/comp.lang.forth/vWHfJSlfHHE/LzId3ClmBwAJ)
posting to clf.  This version relies on a small C library and includes gforth
and iForth FFI to use the library. Any other Forth with a C FFI (including
SwiftForth) could very similarly run these examples.

## build
```
make
```

## usage
```
$ gforth gforth-net.fs broadcast.fs -e chat-server


$ iforth include iforth-net.fs include broadcast.fs chat-server
```
The iForth example assumes a launcher similar to the one suggested in [iForth
for Unix People](https://groups.google.com/d/msg/comp.lang.forth/ovFEONJqmGc/JAe8WTVPAgAJ).

## bugs
These examples are incomplete! I stopped short of the final example that
allowed chat clients to have a nickname. Feel free to submit an example using
the \*-net.fs wordset.
