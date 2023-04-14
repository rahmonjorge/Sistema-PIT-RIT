namespace DatabaseModule.Controllers;

using MongoDB.Driver;
using MongoDB.Bson;

public class Controller<T>
{
    protected IMongoCollection<T> _collection;

    protected Controller(IMongoCollection<T> collection)
    {
        _collection = collection;
    }

    public void Create(T document) => _collection.InsertOne(document);

    public T Read(string key, string value)
    {
        FilterDefinition<T> filter;
        if (key == "_id")
            filter = Builders<T>.Filter.Eq(key, ObjectId.Parse(value));
        else
            filter = Builders<T>.Filter.Eq(key, value);
        
        return _collection.Find(filter).FirstOrDefault();
    }

    public T Read(string key1, string value1, string key2, int value2)
    {
        FilterDefinition<T> filter = 
            Builders<T>.Filter.Eq(key1, value1) &
            Builders<T>.Filter.Eq(key2, value2);
        return _collection.Find(filter).FirstOrDefault();
    }

    public List<T> ReadAll()
    {
        FilterDefinition<T> filter = Builders<T>.Filter.Empty;
        return _collection.Find(filter).ToList();
    }

    public async Task<T> ReadAsync(string key, string value)
    {
        FilterDefinition<T> filter;
        if (key == "_id")
            filter = Builders<T>.Filter.Eq(key, ObjectId.Parse(value));
        else
            filter = Builders<T>.Filter.Eq(key, value);
        
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<T> ReadAsync(string key1, string value1, string key2, string value2)
    {
        FilterDefinition<T> filter = 
            Builders<T>.Filter.Eq(key1, value1) &
            Builders<T>.Filter.Eq(key2, value2);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<T> ReadAsync(string key1, string value1, string key2, int value2)
    {
        FilterDefinition<T> filter = 
            Builders<T>.Filter.Eq(key1, value1) &
            Builders<T>.Filter.Eq(key2, value2);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public List<T> ReadMany(string key, string value)
    {
        FilterDefinition<T> filter;
        if (key == "_id")
            filter = Builders<T>.Filter.Eq(key, ObjectId.Parse(value));
        else
            filter = Builders<T>.Filter.Eq(key, value);
        
        return _collection.Find(filter).ToList();
    }

    public bool Update(string key, string value, T document)
    {
        FilterDefinition<T> filter;
        if (key == "_id")
            filter = Builders<T>.Filter.Eq(key, ObjectId.Parse(value));
        else
            filter = Builders<T>.Filter.Eq(key, value);
        return _collection.ReplaceOne(filter, document).IsAcknowledged;
    }

    public bool Delete(string key, string value)
    {
        FilterDefinition<T> filter;
        if (key == "_id")
            filter = Builders<T>.Filter.Eq(key, ObjectId.Parse(value));
        else
            filter = Builders<T>.Filter.Eq(key, value);
        return _collection.DeleteOne(filter).IsAcknowledged;
    }

    public DeleteResult Delete(string key1, string value1, string key2, string value2)
    {
        FilterDefinition<T> filter = 
            Builders<T>.Filter.Eq(key1, value1) &
            Builders<T>.Filter.Eq(key2, value2);
        return _collection.DeleteOne(filter);
    }
}