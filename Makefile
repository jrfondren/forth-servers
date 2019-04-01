libserverlib.so: serverlib.c
	$(CC) -fPIC -shared -o $@ $<

clean::
	rm -fv libserverlib.so

