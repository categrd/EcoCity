import pandas as pd
import joblib

to_be_scaled = ['Age', 'Education', 'Income', 'Affective Symptoms', 'Rumination', 'Behavioural Symptoms', 
                'Anxiety Personal Impact', 'Attribution Skepticism', 'Impact Skepticism', 
                'Trend Skepticism', 'Response Skepticism']

csv = pd.read_csv('Assets/Scripts/to_classify.csv', names=['Unnamed', 'Age', 'Education', 'Income', 'Male', 'Female', 'Non-binary', 
                                        'Single', 'Married', 'Divorced', 'Widowed', 'Separated', 'Affective Symptoms', 
                                        'Rumination', 'Behavioural Symptoms', 'Anxiety Personal Impact', 
                                        'Attribution Skepticism', 'Impact Skepticism', 'Trend Skepticism', 
                                        'Response Skepticism'])

csv.drop('Unnamed', axis=1, inplace=True)

not_scaled = [column for column in csv.columns if column not in to_be_scaled]

df = csv.iloc[[-1]]

#csv = csv['Age', 'Education', 'Income', 'Male', 'Female', 'Non-binary', 
#                                        'Single', 'Married', 'Divorced', 'Widowed', 'Separated', 'Affective Symptoms', 
#                                        'Rumination', 'Behavioural Symptoms', 'Anxiety Personal Impact', 
#                                        'Attribution Skepticism', 'Impact Skepticism', 'Trend Skepticism', 
#                                        'Response Skepticism']

#data = pd.DataFrame(data=csv, columns=['Age', 'Education', 'Income', 'Male', 'Female', 'Non-binary', 
#                                        'Single', 'Married', 'Divorced', 'Widowed', 'Separated', 'Affective Symptoms', 
#                                        'Rumination', 'Behavioural Symptoms', 'Anxiety Personal Impact', 
#                                        'Attribution Skepticism', 'Impact Skepticism', 'Trend Skepticism', 
#                                        'Response Skepticism'])
print(df)

try:
    scaler = joblib.load('Assets/Scripts/scaler')
    data_scaled = scaler.transform(df[to_be_scaled])
    rf_classifier = joblib.load('Assets/Scripts/classifier')
except FileNotFoundError:
    print('Classifier not found. Please train the classifier first.')

#standardized_data = scaler.transform(data[to_be_scaled])
std_df = pd.DataFrame(data_scaled, columns=to_be_scaled)
data_std = pd.concat((std_df[to_be_scaled[:3]], df[not_scaled], std_df[to_be_scaled[3:]]), axis=1)

print('last row shape: ', df.shape)
pred = rf_classifier.predict(df)
with open('Assets/Scripts/cluster.txt', 'w') as f:
    f.write(str(pred))
print('Pred: ',pred)


