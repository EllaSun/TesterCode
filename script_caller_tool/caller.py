import os,sys
import re
queue = []
class tree:
	def __init__(self):
		self.level=""
		self.name=""

	def setLevel(self,level):
		self.level =  level

	def setName(self, name):
		self.name = name
class Script:
	def __init__(self):
		self.input    = ''
		self.output   = ''
		self.name   = ''

	def set(self, line):
		#cmdRes = re.search('>|>>',line)
		p = re.compile('(sh|python) (.*)')
		match = p.search(line)
		if match:
			line = match.group(2)
			reOne = re.compile('(.*\.(sh|py)) (.*)>(.*)')
			matchOne = reOne.search(line)
			if matchOne:
				self.name = matchOne.group(1)
				self.input = matchOne.group(3)
				self.output = matchOne.group(4)
			else:
				parts = line.split(' ')
				lens = len(parts) - 1 
				if lens >= 0:
					self.name =  parts[0]
				if lens >= 1:
					inputTemp = parts[1:]
					for i in inputTemp:
						self.input = self.input + " " + i
				#if lens >= 2:
				#	self.output = parts[-1]
			self.name=self.name.split('/')[-1]


def findSon(sub):
	#lines = open(path+'/'+sub.name,'r')
	#lines = open(sub.name,'r')
	global queue
	step = 0
	brotherScript = ""

	if os.path.exists(sub.name) == False:
		#os.mknod(sub.name)
		print "!!!!!!" + sub.name + " not exist!!!!!!"
		if queue != []:
			sub=queue[0]
			queue.remove(sub)
			findSon(sub)
	lines = open(sub.name,'r')

	for line in lines:
		line  = line.rstrip()
		line  = line.lstrip()
		conBlack = re.compile(' +')
		line = conBlack.sub(' ', line)
		m     = re.match('(sh|python) ',line)
		if m != None:
			step = step + 1
			level = sub.level + "." + str(step)
			s=Script()
			s.set(line)
			q = tree()
			q.setName(s.name)
			q.setLevel(level)
			queue.append(q)
			num=len(level.split("."))
			print '*' * (20-num*4) + ' ' + level + ' ' + '*' * (20-num*4)
			print "father:" + sub.name  
			print "script:"+str(s.name) 
			print "input:" +str(s.input)
			print "output:" +str(s.output)
			#brotherScript = s.name

	if queue != []:
		sub=queue[0]
		queue.remove(sub)
		findSon(sub)
		
#path = sys.argv[1]
callerScript = sys.argv[1]
root=tree()
root.setName(callerScript)
root.setLevel("1")
findSon(root)
