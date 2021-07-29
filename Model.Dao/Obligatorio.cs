using System.Collections.Generic;

namespace Model.Dao
{
    public interface Obligatorio<cualquierclase>
    {
        void create(cualquierclase obj);
        void delete(cualquierclase obj);
        void update(cualquierclase obj);
        bool find(cualquierclase obj);
        List<cualquierclase> findAll();

    }
}
