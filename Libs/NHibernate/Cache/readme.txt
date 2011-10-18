Documentation and examples are available at www.nhforge.org
Any feedback or issue can be sent to NHibernate user group(http://groups.google.com/group/nhusers) and will be greatly anticipated. 

Up-to-date source code available in the SVN:
http://nhcontrib.svn.sourceforge.net/svnroot/nhcontrib/trunk/src/

Website:
http://sourceforge.net/projects/nhcontrib/

======================================
What is NHibernate.Caches ?
======================================

NHibernate is able to use external caching plugins to minimize the access to the database and improve the performance.
The NHibernate Contrib contains several packages to work with different caching servers and frameworks. 
It's recommended to research for a while before deciding which one is better for you, since some providers require installing adicional services 
(which provides an awesome performance, but might be harder to install in some scenarios)

======================================
Notes
======================================
Build 2.1.2.GA (rev1204 of contrib) for NHibernate 2.1.2GA
================================================================

Build 2.1.1.GA (rev1104 of contrib) for NHibernate 2.1.1GA
================================================================

Build 2.1.0.GA (rev930 of contrib) for NHibernate 2.1.0GA
================================================================

Build 2.1.0.Alpha1 (rev887 of contrib) for NHibernate 2.1.0Beta3
================================================================
** Improvement
    * [NHCH-21] - Allow mnemonic values for priority


** Patch
    * [NHCH-19] - Duplicate expiration property handling in MemCacheClient
    * [NHCH-22] - Enable read of cache.default_expiration from NH configuration

Build 2.0.0.GA for NHibernate 2.0.1GA
=====================================
- In the last release (2.0.0.RC1) there was an error in the documentation about the NH version, this release fixes that minor bug.